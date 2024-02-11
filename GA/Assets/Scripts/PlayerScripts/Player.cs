using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController playerCharacterController;

    [SerializeField] private List<AbilitySO> abilities = new List<AbilitySO>();
    private PlayerMovement2 playerMovement;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement2>();
        playerCharacterController = GetComponent<CharacterController>();

        foreach (var abilitySO in abilities) {
           Debug.Log(abilitySO.name);
        };
    }

    private void Update() {
        playerMovement.Movement();
        playerMovement.Jumping();
        playerMovement.ApplyGravity();


        //handle abilities
        foreach (var abilitySO in abilities) {
            abilitySO.HandleAbility();
        }
    }
}
