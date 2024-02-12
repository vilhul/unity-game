using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New GrapplingGun", menuName = "GrapplingGun")]
public class GrapplingGun : AbilitySO {


    public KeyCode grappleKey = KeyCode.E;

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
                break;
            case GrapplingState.cooldown:
                break;
        }
    }

    private void AttemptGrapple(Player player) {
        Debug.Log("attempting grapple");
        Vector3 origin = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Debug.Log(origin);
        //Vector3 direction = playerCamera.transform.forward;


    }
}
