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

    public enum ChipState {
        Default,
        Browsing
    }

    public ChipState state = ChipState.Default;

    private void Start() {
        
        player = GetComponent<Player>();
        foreach (AbilitySO ability in player.abilities) {
            GameObject abilityChipInstance = Instantiate(abilityChipPrefab, abilityChips.transform.position, abilityChips.transform.rotation);
            abilityChipInstances.Add(abilityChipInstance);
            AbilityChip abilityChipScript = abilityChipInstance.GetComponent<AbilityChip>();
            abilityChipScript.Setup(abilityChips, ability);
        }
    }

    private void Update() {

        if(Input.GetKey(KeyCode.Tab)) {
            state = ChipState.Browsing;
        } else {
            state = ChipState.Default;
        }


        for(int i = 0; i<abilityChipInstances.Count; i++) {
            AbilityChip abilityChipScript = abilityChipInstances[i].GetComponent<AbilityChip>();
            if (abilityChipScript == null) { return; }
            abilityChipScript.UpdatePosition(i, state);
        }
    }
}
