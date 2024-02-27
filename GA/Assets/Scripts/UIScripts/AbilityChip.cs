using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;


public class AbilityChip : MonoBehaviour {

    [SerializeField] private TMP_Text titleTMP;
    [SerializeField] private TMP_Text descriptionTMP;

    public void Setup(Transform abilityChips, AbilitySO ability) {
        transform.SetParent(abilityChips);
        titleTMP.text = ability.name;
        descriptionTMP.text = ability.description;
    }

    public void UpdatePosition(int i, AbilityChipsHandler.ChipState state) {
        if(state == AbilityChipsHandler.ChipState.Default) {
            transform.localPosition = new Vector3(-0.36f, -0.21f + i*0.01f, 0f);
            transform.localEulerAngles = Vector3.zero;
        }

        if(state == AbilityChipsHandler.ChipState.Browsing) {
            transform.localPosition = new Vector3(-0.55f + i*0.55f/3, -0.28f, 0.2f);
            transform.localEulerAngles = new Vector3(-90f, 0f, transform.localPosition.x * 15f / 0.55f);
        }
    }
}


// x=0.55 max/min
// y=-0.28
// z=0.2
// steg = 0.55/3
// rotmax = 15
// rot = x*rotmax/xmax