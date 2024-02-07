using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    [SerializeField] GameObject anchorPoint;
    private SpringJoint springJoint;
    [SerializeField] private Camera playerCamera;
    private float maxGrappleDistance = 25f;
    private bool isGrappling = false;

    // Start is called before the first frame update
    void Start()
    {
        springJoint = GetComponent<SpringJoint>();
        springJoint.maxDistance = float.MaxValue;
        springJoint.spring = 30000;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrappling)
            {
                AttemptGrapple();
            }
            else
            {
                Debug.Log("cancelled");
                springJoint.maxDistance = float.MaxValue;
                isGrappling = false;
            }
        }
            HandleGrappling();
    }

    private void HandleGrappling()
    {
        if(isGrappling && springJoint.maxDistance > 0)
        {
            springJoint.maxDistance -= 10f * Time.deltaTime;
            if(springJoint.maxDistance < 0)
            {
                springJoint.maxDistance = 0;
            }
        }
        
    }

    private void AttemptGrapple()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 direction = playerCamera.transform.forward;
        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxGrappleDistance))
        {
            //grapple hit
            Debug.Log("Grapple hit!");
            Debug.Log(hit.point);
            Debug.Log(hit.distance);

            anchorPoint.transform.position = hit.point;
            springJoint.maxDistance = hit.distance;
            isGrappling = true;
        }
        else
        {
            Debug.Log("Grapple miss :(");
        }

    }
}
