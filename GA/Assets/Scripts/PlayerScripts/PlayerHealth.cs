using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public float currentHealth = 100;


    public void TakeDamage(float damage)
    {
        if (IsOwner)
        {
            currentHealth -= damage; // Reduce health by the specified damage amount

        }

    }
}
