using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] private Vector2 rotation = Vector2.zero;
    [SerializeField] private float speed = 3;
    void Start()
    {
        
    }

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x -= Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector2(rotation.x,0) * speed;
    }
}
