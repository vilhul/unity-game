using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jumping and Gravity")]
    public float gravity = -29.46f;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Grappling hook")]
    [SerializeField] private KeyCode grappleKey = KeyCode.E;
    [SerializeField] private float grappleRange = 15f;
    [SerializeField] private float grapplingAcceleration = 30f;
    private float grapplingVelocity = 0f;
    private bool isGrappling = false;
    private Vector3 grappleAnchor;
    private Vector3 grapplingDirection = Vector3.zero;
    private float grappleDistance;

    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera playerCamera;

    [Header("Misc")]
    [SerializeField] private float groundDistance = 0.4f;

    private Vector3 velocity;

    void Update()
    {
        Movement();
        Jumping();
        ApplyGravity();
        HandleGrappling();
    }
    private bool IsGrounded() {
        //checks if gravity
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask)) {
            return true;
        } else {
            return false;
        }
    }

    private void Movement() {
        //get movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //apply movement relative to rotation
        Vector3 move = transform.right * x + transform.forward * z;

        //check if sprinting and move accordingly
        if(Input.GetKey(sprintKey)) {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        } else {
            controller.Move(move * walkSpeed * Time.deltaTime);
        }
    }

    private void Jumping() {
        //check if jumping
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            //jump
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }
    }

    private void ApplyGravity() {
        //gravity
        if(!isGrappling) {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        //stop applying gravity when grounded
        if (IsGrounded() && velocity.y < 0) {
            velocity.y = -2f;
            grapplingVelocity = 0f;
        }
    }

    private void HandleGrappling() {
        if (Input.GetKeyDown(grappleKey)) {
            if(!isGrappling) {
                Debug.Log("Attemping Grapple");
                AttemptGrapple();
            } else {
                Debug.Log("Disconnecting Grapple");
                isGrappling = false;
            }
        }

        if(isGrappling) {
            //JAG FUCKING HATAR VEKTORER

            grapplingDirection = grappleAnchor - transform.position;
            grapplingVelocity += grapplingAcceleration * Time.deltaTime;


        } else {
            grapplingVelocity -= grapplingAcceleration * 0.1f * Time.deltaTime;
        }
        controller.Move(grapplingDirection.normalized * grapplingVelocity * Time.deltaTime);
    }

    private void AttemptGrapple() {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 direction = playerCamera.transform.forward;
        if(Physics.Raycast(origin, direction, out RaycastHit hitInfo, grappleRange)) {
            Debug.Log("Grapple hit!");
            Debug.Log(hitInfo.point);
            Debug.Log(hitInfo.distance);

            grapplingVelocity = 0;
            grappleAnchor = hitInfo.point;
            grappleDistance = hitInfo.distance;
            isGrappling = true;
        } else {
            Debug.Log("Grapple miss");
        }
    }

}
