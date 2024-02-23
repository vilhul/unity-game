using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityChipsHandler : MonoBehaviour
{

    public enum ChipState {
        Default,
        Browsing,
    }

    private ChipState state = ChipState.Default;

    private Player player;

    private void Start() {
        player = GetComponent<Player>();
    }


    private void Update() {
        switch(state) {
            case ChipState.Default:
                break;
            case ChipState.Browsing:
                break;
        }
    }

}
