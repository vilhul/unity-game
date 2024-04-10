using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class Bullet : NetworkBehaviour
{
    public int damage = 10;
    public float bulletSpeed = 200f;

    private Rigidbody rb;

    public override void OnNetworkSpawn()
    {
             rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = transform.forward * bulletSpeed;
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet has authority
        if (!IsOwner)
            return;
        Debug.Log("destroy bullet");
        // Check if the collision is with a player

        // Destroy the bullet
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        // Check if the bullet has authority
        if (!IsOwner)
            return;

        // Destroy the bullet on the server
        NetworkObject networkObject = GetComponent<NetworkObject>();
        networkObject.Despawn(true);
    }*/
}