using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class Bullet : NetworkBehaviour
{
    public int damage = 10;
    public float bulletSpeed = 20f;

    private Rigidbody rb;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure bullet is owned by the server
        if (IsServer)
        {
            // Enable Rigidbody for physics simulation
            rb.isKinematic = false;
            // Make sure it's moving forward
            rb.velocity = transform.forward * bulletSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet has authority
        if (!IsOwner)
            return;
        Debug.Log("destroy bullet");
        // Check if the collision is with a player
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Apply damage to the player
            playerHealth.TakeDamage(damage);
        }

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
    }
}