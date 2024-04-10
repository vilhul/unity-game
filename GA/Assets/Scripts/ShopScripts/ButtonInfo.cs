using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TMP_Text CostTxt;
    public GameObject ShopManager;
    public AbilitySO ability;

    void Update()
    {
        CostTxt.text = "$" + ShopManager.GetComponent<ShopManager>().shopItems[2,ItemID].ToString();
    }
}
