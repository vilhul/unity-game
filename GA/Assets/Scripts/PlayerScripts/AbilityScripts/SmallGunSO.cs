using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Small Gun", menuName = "SmallGun")]
public class SmallGunSO : AbilitySO
{
    public GameObject gunModel;
    public bool hasSpawnedGun = false;

    private void Awake() {
        hasSpawnedGun = false;
    }

    public override void HandleAbility(Player player) {
        if(!hasSpawnedGun) {
            GameObject gunModelInstance = Instantiate(gunModel, player.transform.position, player.transform.rotation);
            gunModelInstance.transform.SetParent(player.transform.Find("Camera").Find("FirstPersonCamera"));
            gunModelInstance.transform.localPosition = new Vector3(0.769999981f, -0.289999992f, 0.980000019f);
            gunModelInstance.transform.Rotate(0f, 173.800003f, 0f);
            hasSpawnedGun = true;
        }
    }
}
