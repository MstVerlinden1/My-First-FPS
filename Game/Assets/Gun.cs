using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private GunData gunData;
    private PlayerControls inputActions;

    private float timeSinceLastShot;

    private void OnEnable()
    {
        inputActions = new PlayerControls();
        inputActions.PlayerInput.Shoot.Enable();
        inputActions.PlayerInput.Shoot.performed += _ => Shoot();
    }
    private void OnDisable()
    {
        inputActions.PlayerInput.Shoot.Disable();
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot()
    {
        //if we currently have ammo and we canshoot (gun is not reloading + firerate calculated)
        if (gunData.currentAmmo > 0 && CanShoot())
        {
            //shoot out raycast from the gun in the direction of the pivetpoint(ONCE WE HAVE BETTER MODELS PUT ON FORWARDS) with distance in mind return the information to hit
            if (Physics.Raycast(transform.position, -transform.right, out RaycastHit hit, gunData.maxDistance))
            {
                //if whatever we hit has the target script apply damage from the object
                if (hit.transform.GetComponent<Target>())
                {
                    hit.transform.GetComponent<Target>().TakeDamage(gunData.damage);
                }
            }
            gunData.currentAmmo--;
            timeSinceLastShot = 0f;

        }
    }
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }
}
