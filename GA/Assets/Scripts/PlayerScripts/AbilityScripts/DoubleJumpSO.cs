using System.Collections;
using System.Collections.Generic;
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
            player.playerMovement.velocity.y = 0;
            player.playerMovement.velocity.y = Mathf.Sqrt(-2 * player.playerMovement.gravity * player.playerMovement.jumpHeight);
            hasDoubleJumped = true;
        }

    }
}
