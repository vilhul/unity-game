using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New GrapplingGun", menuName = "GrapplingGun")]
public class GrapplingGun : AbilitySO {


    public KeyCode grappleKey = KeyCode.E;
    public float grappleRange = 25f;
    
    private float grappleDistance;
    private float grappleSpeed;
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
                grappleSpeed += 40 * Time.deltaTime;
                player.playerMovement.velocity.y -= player.playerMovement.gravity * Time.deltaTime;
                player.playerMovement.velocity += grapplingDirection.normalized * grappleSpeed * Time.deltaTime;
                Debug.Log(player.playerMovement.velocity.y);

                if(Input.GetKeyDown(grappleKey)) {
                    state = GrapplingState.cooldown;
                }
                break;
            case GrapplingState.cooldown:

                state = GrapplingState.ready;

                break;
        }
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
