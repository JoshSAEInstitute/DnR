using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collectible = 0;

    [SerializeField] private Text collectiblesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            collectible++;
            collectiblesText.text = "Fruits: " + collectible;
        }
    }

}
