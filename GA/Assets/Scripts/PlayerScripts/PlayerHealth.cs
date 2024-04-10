using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public GameObject spectatorCamera;
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>();
    public NetworkVariable<bool> alive = new NetworkVariable<bool>();
    public Vector3 newPosition = new Vector3(0, 400, 0);

    public override void OnNetworkSpawn()
    {
        alive.Value = true;
        currentHealth.Value = 100;
        spectatorCamera = GameObject.FindWithTag("Spectator Camera");
    }


    
    public void Update()
    {
        if (IsOwner)
        {
            Transform objTransform = GetComponent<Transform>();
            if (currentHealth.Value <= 0) {
                alive.Value = false;
                spectatorCamera.GetComponent<Camera>().depth = 50f;
                objTransform.position = newPosition;

            } else
            {

            }

        }
    }
}
