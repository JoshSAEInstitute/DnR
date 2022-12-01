using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    //Storing the OneWayPlatform (OWP) we are standing on
    private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerCollider;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlatform != null)
            {
                //Start coroutine
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if player is standing on the OWP
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            //If we are colliding to a OWP, we are assigning it to our collided object
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            //We stop colliding to the platform, thus we remove the assignment done previously
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        //Store currentOWP collider
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        //Ignore collsion between player and platform
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        //Wait so that the player can fall
        yield return new WaitForSeconds(0.25f);
        //Collision is no longer ignored
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}