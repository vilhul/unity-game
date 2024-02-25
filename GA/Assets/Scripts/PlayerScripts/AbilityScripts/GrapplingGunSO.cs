using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New GrapplingGun", menuName = "GrapplingGun")]
public class GrapplingGunSO : AbilitySO {


    public KeyCode grappleKey = KeyCode.E;
    public float grappleRange = 25f;
    
    private float grappleDistance;
    public float grappleSpeed = 0;
    private Vector3 grappleAnchor;
    private Vector3 grapplingDirection = Vector3.zero;

    enum GrapplingState {
        ready,
        shooting,
        grappling,
        cooldown
    }
    GrapplingState state = GrapplingState.ready;

    

    public override void HandleAbility(Player player) {
           switch (state) {
            case GrapplingState.ready:

                if (Input.GetKeyDown(grappleKey)) {
                    AttemptGrapple(player);
                }

                break;
            case GrapplingState.shooting:
                break;
            case GrapplingState.grappling:

                grapplingDirection = grappleAnchor - player.transform.position;
                grappleSpeed += 40f * Time.deltaTime;

                if(Input.GetKeyDown(grappleKey)) {
                    state = GrapplingState.cooldown;
                }
                break;
            case GrapplingState.cooldown:

                state = GrapplingState.ready;

                break;
        }

        if(state != GrapplingState.grappling) {
            grappleSpeed -= 0.1f * Time.deltaTime;
            if(player.playerMovement.IsGrounded()) {
                grappleSpeed = 0;
            }
        }



        player.playerCharacterController.Move(grappleSpeed * Time.deltaTime * grapplingDirection.normalized);
    }

    private void AttemptGrapple(Player player) {
        Debug.Log("attempting grapple");
        Vector3 origin = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Debug.Log(origin);
        Vector3 direction = player.playerCamera.transform.forward;
        Debug.Log(direction);

        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, grappleRange)) {
            Debug.Log("Grapple hit!");
            Debug.Log(hitInfo.point);
            Debug.Log(hitInfo.distance);

            grappleAnchor=hitInfo.point;
            grappleSpeed = 0;
            state = GrapplingState.grappling;

        } else {
            Debug.Log("Grapple miss");
        }



    }
}
