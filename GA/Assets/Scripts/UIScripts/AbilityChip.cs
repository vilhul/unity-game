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
            //AA DEN NÅR INTE HIT SÅ KOLLA IGENOM DET NÅT ÄR JU LITE GOOFY DÄR
            Debug.Log("bbygurl");
            transform.position = new Vector3(-0.36f, -0.21f + i*0.2f, 0.46f);
        }
    }

}
