using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using Unity.Netcode.Components;
public class Bullet : NetworkBehaviour
{

    public int damage = 10;

    public float bulletSpeed;
    public Rigidbody rb;
    public Vector3 direction;


    private void Start() {
        GetComponent<NetworkTransform>().enabled = true;
    }



    void Update()
    {
        // Move the bullet locally
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }


    /* private void OnCollisionEnter(Collision collision) {
        //här skriver vi kod för att ta skada
        if (!isServer)
            return;

        // Check if the collision is with a player
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Apply damage to the player
            playerHealth.TakeDamage(damage);
        }

        // Destroy the bullet after collision
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    } */
}
