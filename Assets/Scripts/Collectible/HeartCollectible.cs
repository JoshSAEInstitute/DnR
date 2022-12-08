using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{

    private Health gainHealth;

    // Start is called before the first frame update
    void Start()
    {
        gainHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gainHealth.currentHealth < gainHealth.maxHealth)
            {
                gainHealth.currentHealth++;
            }
        }
        Destroy(gameObject);
    }
}
