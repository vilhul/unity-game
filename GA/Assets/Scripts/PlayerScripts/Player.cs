using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public CharacterController playerCharacterController;
    public PlayerMovement2 playerMovement;
    public Camera playerCamera;

    public List<AbilitySO> abilities = new List<AbilitySO>();

    private void Start() {
        if (!IsOwner) return;
        playerMovement = GetComponent<PlayerMovement2>();
        playerCharacterController = GetComponent<CharacterController>();
        playerCamera.enabled = true;
        playerCamera = GetComponentInChildren<Camera>();

        foreach (var abilitySO in abilities) {
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
            abilitySO.HandleAbility(this.GetComponent<Player>());
        }
    }
}
