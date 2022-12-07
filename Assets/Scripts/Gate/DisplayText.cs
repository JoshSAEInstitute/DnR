using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour
{

    public GameObject Object;


    // Start is called before the first frame update
    private void Start()
    {
        Object.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Object.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Object.SetActive(false);
    }
}
