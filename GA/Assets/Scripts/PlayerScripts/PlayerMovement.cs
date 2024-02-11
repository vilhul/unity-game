using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR;

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
    //[SerializeField] private float breakDistance = 1f;
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
    [SerializeField] private Transform gunTip;

    [Header("Misc")]
    [SerializeField] private float groundDistance = 0.4f;

    private Vector3 velocity;
    private LineRenderer lr;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Movement();
        Jumping();
        ApplyGravity();
        HandleGrappling();
    }

    private void LateUpdate() {
        RenderGrapple();
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
        if (IsGrounded() && !isGrappling && velocity.y < 0) {
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
                lr.positionCount = 0;
                isGrappling = false;
            }
        }

        if(isGrappling) {
            //JAG FUCKING HATAR VEKTORER

            grapplingDirection = grappleAnchor - transform.position;
            grapplingVelocity += grapplingAcceleration * Time.deltaTime;

           /* Physics.Raycast(transform.position, grapplingDirection.normalized, out RaycastHit dist, grappleDistance);
            if (dist.distance < breakDistance) {

                Debug.Log("Disconnecting Grapple (too close)");
                lr.positionCount = 0;
                isGrappling = false;
                return;
            } */
            


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

            lr.positionCount = 2;
        } else {
            Debug.Log("Grapple miss");
        }
    }

    private void RenderGrapple() {
        if (!isGrappling) return;

        lr.SetPosition(index: 0, gunTip.position);
        lr.SetPosition(index: 1, grappleAnchor);
    }

}
