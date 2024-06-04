using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingSwing : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] private LayerMask grappleable;
    [SerializeField] private Transform shootPoint, camera, player;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float time, fakeGrappleHoldTimer;
    private SpringJoint joint;
    private Movement playerMove;

    private void Awake()
    {
        playerMove = GetComponent<Movement>();
    }
    private void OnGrapple()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, grappleable))
        {
            if (!hit.transform.GetComponent<GrapplePointLogic>().CanGrapple)
            {
                FakeGrapple();
            }

            playerMove.Freeze = true;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 6f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }
    private void FakeGrapple()
    {
        time += Time.deltaTime;
        if (time > fakeGrappleHoldTimer)
        {
            StopGrapple();
        }
        time = 0;
    }
    private void StopGrapple()
    {
        playerMove.Freeze = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
    private void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, shootPoint.position);
        lr.SetPosition(1, grapplePoint);
    }
}
