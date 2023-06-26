using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private KeyCode dropKey = KeyCode.G;
    [SerializeField] private float trowDistance;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    private Camera playerCam;
    private Collider player;
    private Rigidbody rb;
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
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            rb.isKinematic= true;
        }
    }
    private void DropWeapon()
    {
        if (Input.GetKey(dropKey))
        {
            gameObject.transform.parent = null;
            rb.isKinematic= false;
            rb.AddRelativeForce(Vector3.forward * trowDistance);
        }
    }
    private void shoot()
    {

    }
}
