using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //List of variables
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float mouseSpeed = 5f;
    [SerializeField] private float jump;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector3 mouseInput;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Look();
    }
    private void Look()
    {

    }
    private void Move()
    {
        //get the input
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal") * playerSpeed, rb.velocity.y, Input.GetAxisRaw("Vertical") * playerSpeed);
        //apply the input to movement of the rb
        rb.velocity = moveInput;
        
        //if we press jump button jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }
}
