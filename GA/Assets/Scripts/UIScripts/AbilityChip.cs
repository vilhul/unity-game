using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AbilityChip : MonoBehaviour {

    public TMP_Text titleTMP;
    public TMP_Text toActivateTMP;
    [SerializeField] private TMP_Text descriptionTMP;
    [SerializeField] private TMP_Text timerTMP;
    [SerializeField] private Image imageTMP;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private Material readyMaterial;
    [SerializeField] private Material notReadyMaterial;
    public GameObject selected;


    public void Setup(Transform abilityChips, AbilitySO ability) {
        transform.SetParent(abilityChips);
        titleTMP.text = ability.name;
        descriptionTMP.text = ability.description;
        if(ability.sprite != null ) {
            imageTMP.sprite = ability.sprite;
        }
    }

    public void UpdatePosition(int i, AbilityChipsHandler.ChipState state) {
        if(state == AbilityChipsHandler.ChipState.Default) {
            transform.localPosition = new Vector3(-0.36f, -0.21f + i*0.015f, 0f);
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            gameObject.SetActive(true);
            toActivateTMP.text = ("");
        }

        if(state == AbilityChipsHandler.ChipState.Browsing) {
            transform.localPosition = new Vector3(-0.55f + i*0.55f/3, -0.28f, 0.2f);
            transform.localEulerAngles = new Vector3(-90f, 180f, transform.localPosition.x * 15f / 0.55f);
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            gameObject.SetActive(true);
        }

        if(state == AbilityChipsHandler.ChipState.Selected) {
            gameObject.SetActive(false);
        }
    }

    public void ShowSelected() {
        gameObject.SetActive(true);
        selected.SetActive(false);
        toActivateTMP.text = ("");
        transform.localPosition = new Vector3(0f, 0f, -0.1f);
        transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    public void UpdateTimer(AbilitySO ability) {
        timerTMP.text = Mathf.Ceil(ability.abilityCountdown).ToString();

        if(ability.abilityCountdown == ability.abilityCooldown) {
            timerTMP.text = string.Empty;
            model.material = readyMaterial;
        } else {
            model.material = notReadyMaterial;
        }
    }
}


// x=0.55 max/min
// y=-0.28
// z=0.2
// steg = 0.55/3
// rotmax = 15
// rot = x*rotmax/xmax