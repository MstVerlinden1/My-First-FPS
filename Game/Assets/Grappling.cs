using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private Movement _pm;
    [SerializeField]private GameObject _camera;
    [SerializeField] private GameObject _gunTip;
    [SerializeField] private LayerMask ableToGrapple;

    [Header("Grappling")]
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleDelayTime;
    private bool _grappling;
    private LineRenderer _lineRenderer;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float grapplingCooldown;
    private float grapplingCooldownTimer;

    private void Start()
    {
        _pm = GetComponent<Movement>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }
    private void Update()
    {
        if (grapplingCooldownTimer >= 0)
            grapplingCooldownTimer -= Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (_grappling)
            _lineRenderer.SetPosition(0, _gunTip.transform.position);
    }
    public void OnGrapple()
    {
        StartGrapple();
    }
    private void StartGrapple()
    {
        if (grapplingCooldownTimer >= 0) return;

        _grappling = true; 

        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward,out hit, maxGrappleDistance, ableToGrapple))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = _camera.transform.position + _camera.transform.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(1, grapplePoint);
    }
    private void ExecuteGrapple()
    {

    }
    private void StopGrapple()
    {
        _grappling = false;
        grapplingCooldownTimer = grapplingCooldown;
        _lineRenderer.enabled = false;
    }
}
