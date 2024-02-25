using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "DoubleJump")]
public class DoubleJumpSO : AbilitySO
{
    public bool hasDoubleJumped = false;
    public override void HandleAbility(Player player) {
        
        if (player.playerMovement.IsGrounded()) {
            hasDoubleJumped = false;
            return;
        }
        if(Input.GetButtonDown("Jump") && !hasDoubleJumped) {

            foreach(AbilitySO ability in player.abilities) {
                if(ability is GrapplingGunSO) {
                    GrapplingGunSO grapplingGunSO = (GrapplingGunSO)ability;
                    grapplingGunSO.grappleSpeed = 0;
                    player.playerMovement.velocity = Vector3.zero;
                }
            }
            player.playerMovement.velocity.y = 0;
            player.playerMovement.velocity.y = Mathf.Sqrt(-2 * player.playerMovement.gravity * player.playerMovement.jumpHeight);
            hasDoubleJumped = true;

        }

    }
}
