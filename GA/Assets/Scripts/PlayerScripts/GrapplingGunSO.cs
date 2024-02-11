using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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

    

    public override void HandleAbility() {
           switch (state) {
            case GrapplingState.ready:

                if (Input.GetKeyDown(grappleKey)) {
                    AttemptGrapple();
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

    private void AttemptGrapple() {

    }
}
