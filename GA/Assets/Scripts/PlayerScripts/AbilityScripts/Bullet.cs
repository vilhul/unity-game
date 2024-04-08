using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody rb;
    public Vector3 direction;


    private void Start() {
        Debug.Log(bulletSpeed);
        rb.AddForce(1000f * bulletSpeed * Time.deltaTime * direction.normalized);
    }

    private void OnCollisionEnter(Collision collision) {
        //här skriver vi kod för att ta skada

        Destroy(gameObject);
    }
}
