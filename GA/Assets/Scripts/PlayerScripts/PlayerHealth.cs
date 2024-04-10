using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public GameObject mainCamera;
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>();
    public Vector3 newPosition = new Vector3(0, 400, 0);

    public override void OnNetworkSpawn()
    {
        currentHealth.Value = 100;
        mainCamera = GameObject.FindWithTag("Spectator Camera");
    }


    
    public void Update()
    {
        if (IsOwner)
        {
            Transform objTransform = GetComponent<Transform>();
            if (currentHealth.Value <= 0) { 
                
                objTransform.position = newPosition;

            } else
            {

            }

        }
    }
}
