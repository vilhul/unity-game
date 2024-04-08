using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New GrapplingGun", menuName = "GrapplingGun")]
public class GrapplingGunSO : AbilitySO
{


    public KeyCode grappleKey = KeyCode.E;
    public float grappleRange = 25f;
    public float grappleSpeed = 0;
    public GameObject grapplingGunModel;
    public bool hasSpawnedGrapplingGun;

    private LineRenderer lineRenderer;
    private float grappleDistance;
    private Vector3 grappleAnchor;
    private Vector3 grapplingDirection = Vector3.zero;

    enum GrapplingState
    {
        ready,
        shooting,
        grappling,
        cooldown
    }
    GrapplingState state;



    public override void HandleAbility(PlayerManager player)
    {
        if (!hasSpawnedGrapplingGun)
        {

            GameObject gunModelInstance = Instantiate(grapplingGunModel, player.transform.position, player.transform.rotation);
            gunModelInstance.transform.SetParent(player.transform.Find("Camera").Find("FirstPersonCamera"));
            gunModelInstance.transform.localPosition = new Vector3(-1.47000003f, 0.209999993f, 1.50999999f);
            gunModelInstance.transform.Rotate(new Vector3(0f, 0f, 336.980011f));
            lineRenderer = player.lineRenderer;
            //lineRenderer = player.transform.Find("Camera").Find("FirstPersonCamera").Find("GrapplingGunModel(Clone)").GetComponent<LineRenderer>();

            hasSpawnedGrapplingGun = true;
            state = GrapplingState.ready;
        }



        switch (state)
        {
            case GrapplingState.ready:

                if (Input.GetKeyDown(grappleKey))
                {
                    AttemptGrapple(player);
                }

                break;
            case GrapplingState.shooting:
                break;
            case GrapplingState.grappling:

                grapplingDirection = grappleAnchor - player.transform.position;
                grappleSpeed += 40f * Time.deltaTime;
                RenderGrapple(player);

                if (Input.GetKeyDown(grappleKey))
                {
                    state = GrapplingState.cooldown;
                }
                break;
            case GrapplingState.cooldown:

                state = GrapplingState.ready;

                break;
        }

        if (state != GrapplingState.grappling)
        {
            grappleSpeed -= 0.1f * Time.deltaTime;
            if (player.playerMovement.IsGrounded())
            {
                grappleSpeed = 0;
            }


            lineRenderer.enabled = false;
        }



        player.playerCharacterController.Move(grappleSpeed * Time.deltaTime * grapplingDirection.normalized);
    }

    private void AttemptGrapple(PlayerManager player)
    {
        Debug.Log("attempting grapple");
        Vector3 origin = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Debug.Log(origin);
        Vector3 direction = player.playerCamera.transform.forward;
        Debug.Log(direction);

        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, grappleRange))
        {
            Debug.Log("Grapple hit!");
            Debug.Log(hitInfo.point);
            Debug.Log(hitInfo.distance);

            grappleAnchor = hitInfo.point;
            grappleSpeed = 0;
            state = GrapplingState.grappling;

        }
        else
        {
            Debug.Log("Grapple miss");
        }



    }

    private void RenderGrapple(PlayerManager player)
    {
        lineRenderer.enabled = true;
        //lineRenderer.SetPosition(0, player.transform.Find("PlayerModel").Find("GrappleSpawnPoint").position);
        lineRenderer.SetPosition(0, player.transform.Find("Camera").Find("FirstPersonCamera").Find("GrapplingGunModel(Clone)").Find("GunTip").position);
        lineRenderer.SetPosition(1, grappleAnchor);
    }
}