using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MouseLook : NetworkBehaviour
{

    [SerializeField] private float mouseSensitivty = 100f;
    [SerializeField] private Transform playerBody;
    private float xRotation = 0f;

    private void Start() {
        if (!IsOwner) return;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update() {
        if (!IsOwner) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivty * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivty * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
