using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Small Gun", menuName = "SmallGun")]
public class SmallGunSO : AbilitySO
{
    public GameObject gunModel;


    public override void HandleAbility(Player player) {
        base.HandleAbility(player);
    }
}
