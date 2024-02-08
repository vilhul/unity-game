using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GrapplingGun : MonoBehaviour {

    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("Grappling hook")]
    [SerializeField] private KeyCode grappleKey = KeyCode.E;
    [SerializeField] private float grappleRange = 25f;
    [SerializeField] private float grapplingAcceleration = 40f;
    private float grapplingVelocity = 0f;
    private bool isGrappling = false;
    private Vector3 grappleAnchor;
    private Vector3 grapplingDirection = Vector3.zero;
    private float grappleDistance;

    private Vector3 velocity;
    private LineRenderer lr;
    private float groundDistance = 0.4f;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    private void Update() {
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

    private void HandleGrappling() {
        if (Input.GetKeyDown(grappleKey)) {
            if (!isGrappling) {
                Debug.Log("Attemping Grapple");
                AttemptGrapple();
            } else {
                Debug.Log("Disconnecting Grapple");
                lr.positionCount = 0;
                isGrappling = false;
            }
        }

        if (IsGrounded() && !isGrappling) {
            grapplingVelocity = 0f;
        }


        if (isGrappling) {
            //JAG FUCKING HATAR VEKTORER

            grapplingDirection = grappleAnchor - controller.transform.position;
            grapplingVelocity += grapplingAcceleration * Time.deltaTime;


        } else {
            grapplingVelocity -= grapplingAcceleration * 0.1f * Time.deltaTime;
        }
        controller.Move(grapplingDirection.normalized * grapplingVelocity * Time.deltaTime);
    }
    private void AttemptGrapple() {
        Vector3 origin = new Vector3(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z);
        Vector3 direction = playerCamera.transform.forward;
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, grappleRange)) {
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
