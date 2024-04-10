using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class BulletSpawn : NetworkBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public Transform InistialTransform;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsOwner)
        {
            SpawnBulletServerRpc(InistialTransform.position, InistialTransform.rotation);

        }
    }

    [ServerRpc(RequireOwnership = false)]

    private void SpawnBulletServerRpc(Vector3 position, Quaternion rotation,ServerRpcParams serverRpcParams = default)
    {
        GameObject InstansiatedBullet = Instantiate(bullet, position, rotation);

        InstansiatedBullet.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);

    }
}
