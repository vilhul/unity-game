using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public CharacterController playerCharacterController;
    public PlayerMovement2 playerMovement;
    public Camera playerCamera;
    public LineRenderer lineRenderer;

    public List<AbilitySO> abilities = new List<AbilitySO>();


    public KeyCode selectKey = KeyCode.G;


    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            playerCamera = GetComponentInChildren<Camera>();
            playerCamera.enabled = true;
            playerCamera.depth = 1;

        } else
        {

            playerCamera.depth = 0;
        }


    }

    private void Start() {
        if (!IsOwner) return;
        playerMovement = GetComponent<PlayerMovement2>();
        playerCharacterController = GetComponent<CharacterController>();
        lineRenderer = GetComponent<LineRenderer>();

        foreach (var abilitySO in abilities) {
            abilitySO.abilityCountdown = abilitySO.abilityCooldown;

            if(abilitySO.name == "Small Gun") {
                SmallGunSO smallGunSO = (SmallGunSO)abilitySO;
                smallGunSO.hasSpawnedGun = false;
            }
            if(abilitySO.name == "Grappling Gun") {
                GrapplingGunSO grapplingGunSO = (GrapplingGunSO)abilitySO;
                grapplingGunSO.hasSpawnedGrapplingGun = false;
            }
           Debug.Log(abilitySO.name);
        };
    }

    private void Update() {
        if (!IsOwner) return;
        playerMovement.Movement();
        playerMovement.Jumping();
        playerMovement.ApplyGravity();


        //handle abilities
        foreach (var abilitySO in abilities) {
            abilitySO.HandleAbility(this.GetComponent<PlayerManager>());
        }
    }
}
