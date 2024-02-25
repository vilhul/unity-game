using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

            // OKEJ SÅ DEN HÄR KODEN LYCKAS MED:
            // ATT SPAWNA ETT CHIP PER ABILITY
            // ATT PLACERA DEM SOM BARN TILL ABILITYCHIPS OBJEKTET I HIERARKIN
            
            // AA SÅ VAD BEHÖVS???
            // JO DE SPAWNAR I WORLDSPACE OCH PÅ NÅGOT SÄTT FASTNAR DE DÄR
            // MED ANDRA ORD FÖLJER DE INTE MED NÄR DU GÅR, VILKET ÄR GOOFY FÖR JAG TRODDE DE VÄRDERNA VAR RELATIVA TILL SIN PARENT MEN SÅ ÄR DET UPPENBARLIGN INTE
            // AJA JAG FÅR FIXA DET SEN JAG SKA SOVA NU GODNATT

public class AbilityChip : MonoBehaviour
{
    public enum ChipState {
        Default,
        Browsing,
    }
    ChipState state;

    public void Setup(Transform abilityChips) {
        state = ChipState.Default;
        transform.SetParent(abilityChips);
    }

    public void UpdatePosition(int i) {
        if(state == ChipState.Default) {

            transform.position = new Vector3(-0.36f, -0.21f + i*0.2f, 0.46f);
        }
    }

}
