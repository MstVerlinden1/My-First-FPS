using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private KeyCode dropKey = KeyCode.G;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        DropWeapon();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            gameObject.transform.SetParent(GameObject.Find("Weapons").transform);
            gameObject.transform.localPosition = Vector3.zero;
            rb.isKinematic= true;
        }
    }
    private void DropWeapon()
    {
        if (Input.GetKey(dropKey))
        {
            gameObject.transform.parent = null;
            rb.isKinematic= false;
        }
    }
}
