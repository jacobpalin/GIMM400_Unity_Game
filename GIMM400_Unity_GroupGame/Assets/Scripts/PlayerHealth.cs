using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerHealth : MonoBehaviour
{
    //public float playerHealth;
    public int maxHealth = 5;
    public int currentHealth;
    //public Image healthBar;
    //public GameObject UI;
    public event Action<float> OnHealthPctChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = 3.0f;
        currentHealth = maxHealth;
        //UI = Instantiate(UI, transform.position, transform.rotation);
        //GameObject healthBar = GameObject.FindGameObjectWithTag("Player1Health");
        //if(imageObject != null)
        // {
        //   healthBar = imageObject.GetComponent<imageObject>();
        //}
    }

    // Update is called once per frame
    //void Update()
    //{
    //healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 3);
    //}
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
        
        //add hurt animation here
        //animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("someone died!");
        //Add Death animation here
        //animator.SetBool("IsDead", true);
        //disable other player
        Destroy(gameObject);
        this.enabled = false;
        //GetComponent<Rigidbody>().enabled = false;
        //GetComponent<CharacterController>().enabled = false;
    }
}
