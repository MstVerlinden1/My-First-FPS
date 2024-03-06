using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    void Update()
    { //this isnt so hard it follows the player
        transform.position = cameraPos.position;
    }
}
