using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;

    private SpriteRenderer sprite;

    [SerializeField] private int damage;

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
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Hi");
            DealDamage(target);
        }else if(collision.gameObject.tag == "Enemy")
        {
            DealDamageEnemy(target);
        }
        Destroy(gameObject);

    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<Health>();
        if (atm != null)
        {
            atm.TakeDamage(damage);
        }
    }

    public void DealDamageEnemy(GameObject target)
    {
        var atm = target.GetComponent<Enemy_Shoot>();
        if (atm != null)
        {
            atm.EnemyTakeDamage(damage);
        }
    }

}
