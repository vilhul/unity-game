using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class Bullet : NetworkBehaviour
{
    public int damage = 10;
    public float bulletSpeed = 300f;
    public float destroyDelay = 15f;
    private Rigidbody rb;

    public override void OnNetworkSpawn()
    {
             rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = transform.forward * bulletSpeed;
            if (IsServer) { Destroy(gameObject, destroyDelay); }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet has authority
        if (!IsOwner)
            return;
        Debug.Log("destroy bullet");
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
    }
}