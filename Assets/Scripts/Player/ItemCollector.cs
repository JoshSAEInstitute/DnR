using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collectible = 0;
    private Health gainHealth;
    [SerializeField] private int regainHealth;

    [SerializeField] private Text collectiblesText;

    private void Start()
    {
        gainHealth = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Health Collectible"))
        {
            Debug.Log("I've collided with a heart");
            if (gainHealth.currentHealth < gainHealth.maxHealth)
            {
                Debug.Log("Health less than Max Health");

                gainHealth.GainHealth(regainHealth);
                Destroy(collision.gameObject);
                
                
            }
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectible++;
            collectiblesText.text = "Fruits: " + collectible;
        }
    }

}
