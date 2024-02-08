using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Camera")]
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform orientation;
    private float desiredX;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    private float xRotation;

    [Header("Input")]
    private float x;
    private float y;
    private float moveSpeed = 4500;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void FixedUpdate() {
        Movement();
    }


    private void Movement () {
        ApplyMovementForces();
    }

    private void MyInput () {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void Look() {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void ApplyMovementForces() {
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime);
    }
}
