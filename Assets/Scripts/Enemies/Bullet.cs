using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;

    private SpriteRenderer sprite;

    //All the bullets calculations are made as soon as it spawns
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position);

        //The bullet sprite is flipped depending on where the enmy is facing
        if(moveDir.x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        bulletRB.velocity = new Vector2 (moveDir.x, 0f).normalized * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hi");
        }
        Destroy(gameObject);

    }

}
