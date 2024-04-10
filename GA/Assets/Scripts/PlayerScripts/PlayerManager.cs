using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : NetworkBehaviour
{
    public int score;

    public CharacterController playerCharacterController;
    public PlayerMovement2 playerMovement;
    public Camera playerCamera;
    public Camera firstPersonPlayerCamera;
    public LineRenderer lineRenderer;

    public List<AbilitySO> abilities = new List<AbilitySO>();

    public int chips = 50;
    public ShopManager shopManager;
    public bool isShopping = false;
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>();
    
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

        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }

    private void Update() {
        if (!IsOwner) return;
        if (GameManager.Instance.gameState != GameManager.GameState.playing) return;
        playerMovement.Movement();
        playerMovement.Jumping();
        playerMovement.ApplyGravity();
        foreach (AbilitySO abilitySO in abilities) {
            abilitySO.HandleAbility(this.GetComponent<PlayerManager>());
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
        isReady.Value = false;
        shopManager.canvas.SetActive(true);
        shopManager.Open();
    }

    public void LoadAbilities() {
        //remove all models
        
        foreach (Transform child in firstPersonPlayerCamera.transform) {
            if(child.gameObject.name != "AbilityChips") {
                Destroy(child.gameObject);
            }
        }

        foreach (AbilitySO abilitySO in abilities) {

            abilitySO.abilityCountdown = abilitySO.abilityCooldown;

            //spawn models
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
