using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Small Gun", menuName = "SmallGun")]
public class SmallGunSO : AbilitySO
{
    public GameObject gunModel;
    public GameObject bullet;
    public bool hasSpawnedGun = false;
    public float bulletSpeed;

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

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject bulletInstance = Instantiate(bullet, player.transform.Find("Camera").position + player.transform.Find("Camera").forward, player.transform.Find("Camera").rotation);
            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            bulletScript.direction = player.transform.Find("Camera").forward;
            bulletScript.transform.Rotate(90f, 0f, 0f);
            bulletScript.bulletSpeed = 200f;
        }
    }
}
