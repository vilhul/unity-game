using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityChipsHandler : MonoBehaviour
{

    [SerializeField] GameObject abilityChipPrefab;
    [SerializeField] Transform abilityChips;

    private Player player;
    private List<GameObject> abilityChipInstances = new List<GameObject> ();

    private void Start() {
        
        player = GetComponent<Player>();
        foreach (AbilitySO ability in player.abilities) {
            Debug.Log(ability.name);
            GameObject abilityChipInstance = Instantiate(abilityChipPrefab, abilityChips.position, Quaternion.identity);
            abilityChipInstances.Add(abilityChipInstance);
            AbilityChip abilityChipScript = abilityChipInstance.GetComponent<AbilityChip>();
            abilityChipScript.Setup();
        }
    }

    private void Update() {
        for(int i = 0; i<abilityChipInstances.Count; i++) {
            AbilityChip abilityChipScript = abilityChipInstances[i].GetComponent<AbilityChip>();
            if (abilityChipScript != null) { return; }
            abilityChipScript.UpdatePosition(i);
        }
    }
}
