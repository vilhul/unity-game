using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject firstPersonCanvas;
    public GameObject LobbyUi;
    void Start()
    {
        // Initially hide the menu
        firstPersonCanvas.SetActive(false);
        LobbyUi.SetActive(true);
    }

    void Update()
    {


        // Check if the client is connected to a server
        bool isConnected = NetworkManager.Singleton.IsConnectedClient;
        // Show or hide the menu based on the connection status
        LobbyUi.SetActive(!isConnected);
        firstPersonCanvas.SetActive(isConnected);
    }
}