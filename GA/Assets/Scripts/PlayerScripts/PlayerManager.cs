using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : NetworkBehaviour
{
    public string playerName;

    public CharacterController playerCharacterController;
    public PlayerMovement playerMovement;
    public Camera playerCamera;
    public Camera firstPersonPlayerCamera;
    public LineRenderer lineRenderer;

    public List<AbilitySO> abilities = new List<AbilitySO>();

    public int chips = 0;
    public ShopManager shopManager;
    public bool isShopping = false;
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>();
    
    public KeyCode selectKey = KeyCode.G;
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;
    public LayerMask obstacleLayer;

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
            SpawnPlayer();

        }

    }

    public void SpawnPlayer()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        transform.position = randomSpawnPosition;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;
        bool foundValidSpawn = false;

        // Try to find a valid spawn position within the spawn area
        int maxAttempts = 40; // Maximum number of attempts to find a valid spawn position
        int attempts = 0;
        do
        {
            float randomX = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2f, spawnAreaCenter.x + spawnAreaSize.x / 2f);
            float randomY = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2f, spawnAreaCenter.y + spawnAreaSize.y / 2f);
            float randomZ = Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2f, spawnAreaCenter.z + spawnAreaSize.z / 2f);

            randomPosition = new Vector3(randomX, randomY, randomZ);

            // Check if there are any obstacles at the random position
            if (!Physics.CheckBox(randomPosition, Vector3.one * 0.5f, Quaternion.identity, obstacleLayer) &&
                randomZ > -30 && randomZ < 30)
            {
                foundValidSpawn = true;
            }

            attempts++;
        } while (!foundValidSpawn && attempts < maxAttempts);

        // If no valid spawn position was found, use the center of the spawn area
        if (!foundValidSpawn)
        {
            Debug.LogWarning("Failed to find a valid spawn position. Spawning at the center of the spawn area.");
            randomPosition = spawnAreaCenter;
        }

        return randomPosition;
    }


    private void Start() {
        if (!IsOwner) return;
        playerMovement = GetComponent<PlayerMovement>();
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


    public void UpdateIsReady()
    {
        if (IsServer)
        {
            isReady.Value = true;
        }
    }
}
