using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class AbilityHandler : MonoBehaviour {
    [SerializeField] private AbilitySO abilitySO;

    [SerializeField] private TMP_Text titleTMP;
    [SerializeField] private TMP_Text descriptionTMP;

    private void Start() {
        string title = abilitySO.name;
        string description = abilitySO.description;
        titleTMP.text = title;
        descriptionTMP.text = description;
    }
}
