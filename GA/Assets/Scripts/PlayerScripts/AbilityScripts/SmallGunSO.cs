using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Small Gun", menuName = "SmallGun")]
public class SmallGunSO : AbilitySO
{
    public GameObject gunModel;
    private bool hasSpawnedGun = false;

    private void Awake() {
        hasSpawnedGun = false;
    }

    public override void HandleAbility(Player player) {
        if(!hasSpawnedGun) {
            Debug.Log(gunModel.name);
            Debug.Log(player.name);
            GameObject gunModelInstance = Instantiate(gunModel, player.transform.position, player.transform.rotation);
            gunModelInstance.transform.SetParent(player.transform.Find("Camera").Find("FirstPersonCamera"));
            Debug.Log(player.transform.name);
            Debug.Log(gunModelInstance.name);
            Debug.Log("AAAAAAAAAA");
            hasSpawnedGun = true;
        }
    }
}
