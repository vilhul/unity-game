using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class AbilityChip : MonoBehaviour
{
    public enum ChipState {
        Default,
        Browsing,
    }

    ChipState state;

    public void Setup() {
        state = ChipState.Default;
    }

    public void UpdatePosition(int i) {
        if(state == ChipState.Default) {
            //AA DEN N�R INTE HIT S� KOLLA IGENOM DET N�T �R JU LITE GOOFY D�R
            Debug.Log("bbygurl");
            transform.position = new Vector3(-0.36f, -0.21f + i*0.2f, 0.46f);
        }
    }

}
