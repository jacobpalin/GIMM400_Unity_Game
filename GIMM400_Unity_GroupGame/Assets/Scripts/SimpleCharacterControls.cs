using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterControls : MonoBehaviour
{
    public GameObject currentWeapon;
    public GameObject currentWeaponProjectile;
    [SerializeField] private GameObject firePoint;

    [SerializeField] private float cooldown;
    private float timer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && timer <= 0)
        {
            timer = cooldown;
            Instantiate(currentWeaponProjectile, firePoint.transform.position, firePoint.transform.rotation);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        /*//if weapon picked up, change currentWeapon
         * then set currentWeaponProjectile to the respective projectile on that weapon
        */
    }
}