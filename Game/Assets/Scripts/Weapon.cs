using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Collider>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            gameObject.transform.parent = other.transform;
        }
    }
}
