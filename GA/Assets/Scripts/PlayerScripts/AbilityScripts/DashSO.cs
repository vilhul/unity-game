using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dash", menuName = "Dash")]
public class DashSO : AbilitySO
{

    public KeyCode dashKey = KeyCode.F;
    public float dashRange = 15f;

    enum DashState {
        ready,
        dashing,
        cooldown
    }
    DashState state = DashState.ready;

    public override void HandleAbility(Player player) {
        switch(state) {
            case DashState.ready:

                if(Input.GetKeyDown(dashKey)) {
                    state = DashState.dashing;
                }

                break;
            case DashState.dashing:

                player.playerCharacterController.Move(player.playerCamera.transform.forward * dashRange);
                state = DashState.cooldown;

                break;
            case DashState.cooldown:
                state = DashState.ready;
                break;
        }
    }
}
