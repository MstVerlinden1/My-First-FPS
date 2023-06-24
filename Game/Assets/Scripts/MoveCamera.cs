using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
