using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleUIRenderer : MonoBehaviour
{
    public RectTransform canvasRect;

    private void Awake() {
        Canvas canvas = FindObjectOfType<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

}
