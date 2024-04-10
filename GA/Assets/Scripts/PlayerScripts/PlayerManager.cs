using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : NetworkBehaviour
{
    public CharacterController playerCharacterController;
    public PlayerMovement2 playerMovement;
    public Camera playerCamera;
    public Camera firstPersonPlayerCamera;
    public LineRenderer lineRenderer;

    public List<AbilitySO> abilities = new List<AbilitySO>();

    public int chips = 50;
    public GameObject shopCanvas;
    public bool isShopping = false;

    public KeyCode selectKey = KeyCode.G;
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {

            firstPersonPlayerCamera.enabled = true;
            firstPersonPlayerCamera.depth = 1;
            playerCamera = GetComponentInChildren<Camera>();
            playerCamera.enabled = true;
            playerCamera.depth = 1;

        } else
        {

            playerCamera.depth = 0;
            firstPersonPlayerCamera.depth = 0;
        }
        if (IsLocalPlayer)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            transform.position = randomSpawnPosition;
        }

    }


    private Vector3 GetRandomSpawnPosition()
    {
        // Calculate random position within spawn area
        float randomX = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.x + spawnAreaSize.x / 2f);
        float randomY = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2f, spawnAreaCenter.y + spawnAreaSize.y / 2f);
        float randomZ = Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2f, spawnAreaCenter.z + spawnAreaSize.z / 2f);

        return new Vector3(randomX, randomY, randomZ);
    }

    private void Start() {
        if (!IsOwner) return;
        playerMovement = GetComponent<PlayerMovement2>();
        playerCharacterController = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();

        LoadAbilities();

        shopCanvas = GameObject.Find("ShopCanvas");
        shopCanvas.SetActive(false);
    }

    private void Update() {
        if (!IsOwner) return;
        playerMovement.Movement();
        playerMovement.Jumping();
        playerMovement.ApplyGravity();
        Debug.Log(GetComponent<PlayerHealth>().currentHealth.Value);

        //handle abilities
        foreach (var abilitySO in abilities) {
            abilitySO.HandleAbility(this.GetComponent<PlayerManager>());
        }


        if(Input.GetKeyDown(KeyCode.P)) {
           // OpenShop();
        }

    }


    private void OnTriggerEnter(Collider collider)
    {
        if (!IsServer) return;

        if (collider.GetComponent<Bullet>() && GetComponent<NetworkObject>().OwnerClientId != collider.GetComponent<NetworkObject>().OwnerClientId)
        {
            
            GetComponent<PlayerHealth>().currentHealth.Value -= 10;
            Debug.Log("tests");
        }
    }


    public void OpenShop() {
        if (!IsOwner) return;
        isShopping = true;
        shopCanvas.SetActive(true);
        GameObject.Find("ShopManager").GetComponent<ShopManager>().Open();
    }

    public void LoadAbilities() {

        foreach (var abilitySO in abilities) {

            abilitySO.abilityCountdown = abilitySO.abilityCooldown;

            if (abilitySO.name == "Small Gun") {
                SmallGunSO smallGunSO = (SmallGunSO)abilitySO;
                smallGunSO.hasSpawnedGun = false;
            }
            if (abilitySO.name == "Grappling Gun") {
                GrapplingGunSO grapplingGunSO = (GrapplingGunSO)abilitySO;
                grapplingGunSO.hasSpawnedGrapplingGun = false;
            }
            Debug.Log(abilitySO.name);
        };

    }
}
