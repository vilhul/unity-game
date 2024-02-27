using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;



// OKEJ S� SKILLNADEN MELLAN DEN H�R OCH PLAYERMOVEMENT (1) �R ATT DEN H�R SAKNAR GRAPPLE KOD, F�R GRAPPLE KOD �R I ETT EGET OBJEKT
// MEN PLAYERMOVEMENT SKRIPTET �R KVAR F�R REFERENS N�R JAG FORTS�TTER LEKA MED GRAPPLE HOOKEN. DEN SKA RYKA S� SM�NINGOM

public class PlayerMovement2 : NetworkBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("Jumping and Gravity")]
    public float gravity = -29.46f;
    public float jumpHeight = 3f;
    
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Misc")]
    [SerializeField] private float groundDistance = 0.4f;

    public Vector3 velocity;

    public bool IsGrounded() {

        //checks if gravity
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask)) {
            return true;
        } else {
            return false;
        }
    }

    public void Movement() {
        if (!IsOwner) return;
        //get movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //apply movement relative to rotation
        Vector3 move = transform.right * x + transform.forward * z;

        //check if sprinting and move accordingly
        if (Input.GetKey(sprintKey)) {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        } else {
            controller.Move(move * walkSpeed * Time.deltaTime);
        }
    }

    public void Jumping() {
        if (!IsOwner) return;
        //check if jumping
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            //jump
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
    }

    public void ApplyGravity() {
        //gravity
        if (!IsOwner) return;
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);

        //stop applying gravity when grounded
        if (IsGrounded() && velocity.y < 0) {
            velocity.y = -2f;
        }
    }
}
