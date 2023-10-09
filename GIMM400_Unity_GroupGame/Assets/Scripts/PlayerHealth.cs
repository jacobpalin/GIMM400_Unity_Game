using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        GameObject healthBar = GameObject.FindGameObjectWithTag("Player1Health");
      // if(imageObject != null)
        //{
          //  healthBar = imageObject.GetComponent<imageObject>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 3);   
    }
}
