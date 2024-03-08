using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject camHolder;
    [SerializeField] private float speed, sensitivity, maxForce, jumpForce;
    private Vector2 move, look;
    private float lookRotation;
    [SerializeField] private bool grounded;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        Look();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camHolder = transform.GetChild(0).gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Move()
    {
        //Find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;
        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);
        //calculate forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        //limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);
        //apply force
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    private void Look()
    {
        //turn
        transform.Rotate(Vector3.up * look.x * sensitivity);
        //look
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }
    private void Jump()
    {
        Vector3 jumpForces = Vector3.zero;
        GroundCheck();
        //if grounded is true jump
        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;

            rb.AddForce(jumpForces, ForceMode.VelocityChange);
        }
    }
    private void GroundCheck()
    {
        //store raycast information
        RaycastHit hit;
        //shoot raycast(sphere) and if its true run true else false
        if (Physics.SphereCast(GetComponent<CapsuleCollider>().transform.position, GetComponent<CapsuleCollider>().height / 4, Vector3.down, out hit, GetComponent<CapsuleCollider>().height / 4 + 0.01f))
        {
            grounded = true;
            print(hit.distance);
        }
        else
        {
            grounded = false;
        }
    }
}
