using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityChipsHandler : MonoBehaviour
{

    [SerializeField] GameObject abilityChipPrefab;
    [SerializeField] Transform abilityChips;

    private Player player;
    private List<GameObject> abilityChipInstances = new List<GameObject> ();
    private int selectedChip = 0;
    private bool hasScrolled = false;


    public enum ChipState {
        Default,
        Browsing,
        Selected
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
        for(int i = 0; i<abilityChipInstances.Count; i++) {
            AbilityChip abilityChipScript = abilityChipInstances[i].GetComponent<AbilityChip>();
            if (abilityChipScript == null) { return; }
            abilityChipScript.UpdatePosition(i, state);
        }

        switch(state) {
            case ChipState.Default:
                for (int i = 0; i < abilityChipInstances.Count; i++) {
                    abilityChipInstances[i].GetComponent<AbilityChip>().selected.SetActive(false);
                }

                if (Input.GetKey(KeyCode.Tab)) {
                    state = ChipState.Browsing;
                }
                break;
            case ChipState.Browsing:
                if(!Input.GetKey(KeyCode.Tab)) {
                    state = ChipState.Default;
                    hasScrolled = false;
                }

                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if(!hasScrolled) {
                    if(scroll != 0f) {
                        hasScrolled = true;
                        selectedChip = 0;
                    }
                } else {
                    if (scroll != 0f) {
                        hasScrolled = true;
                        selectedChip += (int)Mathf.Sign(scroll);
                        selectedChip = Mathf.Clamp(selectedChip, 0, abilityChipInstances.Count - 1);
                    }

                    
                    for(int i = 0;  i < abilityChipInstances.Count; i++) {
                        if(selectedChip == i) {
                            abilityChipInstances[i].GetComponent<AbilityChip>().selected.SetActive(true);
                        } else {
                            abilityChipInstances[i].GetComponent<AbilityChip>().selected.SetActive(false);
                        }
                    }
                    



                    if (Input.GetKeyDown(player.selectKey)) {
                        state = ChipState.Selected;
                    }
                }
                break;
            case ChipState.Selected:
                AbilityChip abilityChipScript = abilityChipInstances[selectedChip].GetComponent<AbilityChip>();
                if (abilityChipScript == null) {
                    state = ChipState.Default;
                    return;
                }
                abilityChipScript.ShowSelected();


                if(Input.GetKeyDown(player.selectKey)) {
                    state = ChipState.Default;
                    hasScrolled = false;
                }
                break;
        }
    }
}
