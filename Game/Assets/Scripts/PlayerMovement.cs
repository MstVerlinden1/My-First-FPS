using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //List of variables
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jump;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool grounded;
    [SerializeField] private float groundDrag;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;

        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //groundcheck
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        PlayerInput();
        SpeedControl();

        //control drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    void FixedUpdate()
    {
        Move();
    }
    private void PlayerInput()
    {
        //get the input
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void Move()
    {
        //apply the input to movement of the rb
        moveDirection = orientation.forward * moveInput.z + orientation.right * moveInput.x;
        rb.AddForce(moveDirection.normalized * playerSpeed * 10f, ForceMode.Force);
        
        //if we press jump button jump
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        //}
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0,rb.velocity.z);
        //limit velocity if needed
        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
