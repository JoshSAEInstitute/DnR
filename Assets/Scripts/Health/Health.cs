using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Maximum amount of health an object can have
    public int maxHealth;
    //The current health of the object
    public int currentHealth;


    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        //Allows the object's health to be reduced

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
