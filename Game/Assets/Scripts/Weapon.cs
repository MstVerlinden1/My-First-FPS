using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private KeyCode dropKey = KeyCode.G;
    [SerializeField] private float trowDistance;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private Camera playerCam;
    [SerializeField] private bool onPlayer;
    [SerializeField] private float impactForce = 30f;
    private Collider player;
    private Rigidbody rb;
    void Start()
    {
        //get componets so i dont have to drag them around in the editor :skull:
        player = GameObject.Find("Player").GetComponent<Collider>();
        playerCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        //ignore physical collition between the gun and the player(so we dont walk over a weapon moving the player up in Y)
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        //if we press the drop button drop gun on floor
        if (Input.GetKey(dropKey))
        {
            DropWeapon();
        }
        //if we press the fire button and the gun is being held shoot!
        if (Input.GetButtonDown("Fire1") && onPlayer)
        {
            shoot();
        }
    }
    private void OnTriggerEnter(Collider other)
    { //if player walk into the gun make it a child set the position and rotation, make it kinematic and set the onplayer var to true
        if (other.gameObject.layer == 6)
        {
            gameObject.transform.SetParent(GameObject.Find("Weapons").transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            rb.isKinematic= true;
            onPlayer= true;
        }
    }
    private void DropWeapon()
    { //remove the weapon as a child of the player set kinematic off so gravity works again add some force so the player trows it and set the var onplayer on false
        gameObject.transform.parent = null;
        rb.isKinematic= false;
        rb.AddRelativeForce(Vector3.forward * trowDistance);
        onPlayer = false;
    }
    private void shoot()
    { //shoot a raycast from the player camera and debug what we hit
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }


            if (hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }
        }
    }
}
