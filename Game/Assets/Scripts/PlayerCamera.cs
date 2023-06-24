using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 mouseInput;
    [SerializeField] private Transform orientation;
    [SerializeField] private Vector2 sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensitivity.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensitivity.y;
        mouseInput.y += mouseX;
        mouseInput.x -= mouseY;
        //stop rotation at the top and bottom
        mouseInput.x = Mathf.Clamp(mouseInput.x, -90f, 90f);
        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(mouseInput.x, mouseInput.y, 0);
        orientation.rotation = Quaternion.Euler(0,mouseInput.y,0);
    }
}
