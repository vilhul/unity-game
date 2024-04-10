using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int[,] shopItems = new int[3, 4];
    public TMP_Text chipsTXT;
    public GameObject canvas;
    public PlayerManager playerManager;

    private void Start() {

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;

        //Cost
        shopItems[2, 1] = 5;
        shopItems[2, 2] = 3;
        shopItems[2, 3] = 8;
    }


    public void Open()
    {
        Cursor.lockState = CursorLockMode.Confined;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            PlayerManager playerScript = player.GetComponent<PlayerManager>();
            if(playerScript.isShopping) {
                playerManager = playerScript;
            }
        }
        chipsTXT.text = "Chips: " + playerManager.chips.ToString();
    }

    
    
    public void Buy() {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(playerManager.chips >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]) {
            playerManager.chips -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            playerManager.abilities.Add(ButtonRef.GetComponent<ButtonInfo>().ability);
            ButtonRef.SetActive(false);

            chipsTXT.text = "Chips: " + playerManager.chips.ToString();
        }
    }

    public void Ready() {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            PlayerManager playerScript = player.GetComponent<PlayerManager>();
            if (playerScript.isShopping) {
                player.GetComponent<PlayerManager>().abilities = playerManager.abilities;
                player.GetComponent<PlayerManager>().chips = playerManager.chips;
                player.GetComponent<PlayerManager>().isShopping = false;
                player.GetComponent<PlayerManager>().LoadAbilities();
                player.GetComponent<AbilityChipsHandler>().LoadAbilityChips();
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);

        //kod för att invänta de andra spelararna på något smidigt sätt
    }

}
