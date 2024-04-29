using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private GameObject _camera;
    [SerializeField] private GameObject _gunTip;
    [SerializeField] private LayerMask ableToGrapple;
    private PhysicMaterial _physicMaterial;
    private Movement _pm;
    private Rigidbody _rb;

    [Header("Grappling")]
    [SerializeField] private float overshootYAxis;
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float grappleDelayTime;
    private bool _grappling;
    private LineRenderer _lineRenderer;

    private Vector3 grapplePoint;
    private Vector3 velocityToSet;

    [Header("Cooldown")]
    [SerializeField] private float grapplingCooldown;
    private float grapplingCooldownTimer;

    private void Start()
    {
        _physicMaterial = GetComponent<PhysicMaterial>();
        _pm = GetComponent<Movement>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _rb = GetComponent<Rigidbody>();
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
        _pm.Freeze = true;
        _rb.drag = 0;

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
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        JumpToPostion(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }
    private void StopGrapple()
    {
        _grappling = false;
        grapplingCooldownTimer = grapplingCooldown;
        _lineRenderer.enabled = false;
        _pm.Freeze = false;
    }
    public void JumpToPostion(Vector3 targetPosition, float trajectoryHeight)
    {
        _grappling = true;
        _pm.Freeze = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);
    }
    private void SetVelocity()
    {
        _rb.velocity = velocityToSet;
    }
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocotyY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocotyY;
    }
}
