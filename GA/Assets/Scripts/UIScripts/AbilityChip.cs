using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

            // OKEJ S� DEN H�R KODEN LYCKAS MED:
            // ATT SPAWNA ETT CHIP PER ABILITY
            // ATT PLACERA DEM SOM BARN TILL ABILITYCHIPS OBJEKTET I HIERARKIN
            
            // AA S� VAD BEH�VS???
            // JO DE SPAWNAR I WORLDSPACE OCH P� N�GOT S�TT FASTNAR DE D�R
            // MED ANDRA ORD F�LJER DE INTE MED N�R DU G�R, VILKET �R GOOFY F�R JAG TRODDE DE V�RDERNA VAR RELATIVA TILL SIN PARENT MEN S� �R DET UPPENBARLIGN INTE
            // AJA JAG F�R FIXA DET SEN JAG SKA SOVA NU GODNATT

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
