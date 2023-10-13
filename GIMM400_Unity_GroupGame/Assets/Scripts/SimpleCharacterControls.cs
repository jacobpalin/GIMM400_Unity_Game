using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterControls : MonoBehaviour
{
    public GameObject currentWeapon;
    public float projectileSpeed = 50;
    void Update()
    {
        //simple movement
        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float speed = 5.0f;

        transform.position = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;*/

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(currentWeapon, this.transform.position, Quaternion.identity);
            currentWeapon.GetComponent<WeaponTest>().Fire(projectileSpeed, transform.forward);
        }
    }
}
