using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{


    [SerializeField] private float projectileSpeed;

    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }
    public void Fire(float speed)
    {
        projectileSpeed = speed;
    }

    private void Update()
    {
        this.transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }
}