using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    //Enemy speed
    public float speed;

    //Enemy line of sight
    public float lineOfSight;
    private Transform player;

    //Faceplayer
    private Vector3 tempScale;

    //Animation
    private Animator anim;

    //Shooting
    public float shootingRange;
    public float fireRate = 1;
    private float nextFire;

    public GameObject bullet;
    public GameObject bulletParent;

    //Health
    public int maxHP;
    public int currentHP;

    //Behaviour
    public enum behaviour { idle, approach, shoot}
    public behaviour enemyState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        currentHP = maxHP;

    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        switch (enemyState)
        {
            case behaviour.idle:

                anim.SetBool("moving", false);
                //Debug.Log("I'm idling");

                //Check if can shoot
                if (nextFire < Time.time && distanceFromPlayer <= shootingRange)
                {
                    enemyState = behaviour.shoot;
                }

                //--- to Approach
                if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
                {
                    enemyState = behaviour.approach;
                }

                break;
            
            case behaviour.approach:

                //Run Anim
                anim.SetBool("moving", true);
                //Debug.Log("I'm approaching");

                //Object approaches player
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

                //--- to Idle
                if (distanceFromPlayer > lineOfSight)
                {
                    enemyState = behaviour.idle;
                }

                //--- to Shoot
                if (distanceFromPlayer <= shootingRange)
                {
                    enemyState = behaviour.shoot;
                }

                break;

            case behaviour.shoot:

                //Debug.Log("I'm shooting");

                if (nextFire < Time.time)
                {
                    //Shoot Anim
                    anim.SetTrigger("rangedAttack");

                    //Enemy shoot rate
                    nextFire = Time.time + fireRate;
                } 
                else
                {
                    //--- to Idle until it can shoot
                    enemyState = behaviour.idle;
                }

                //--- to Approach
                if (distanceFromPlayer >= shootingRange)
                {
                    enemyState = behaviour.approach;
                }

                break;

        }

        /*

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            //Run Anim
            anim.SetBool("moving", true);

            //Object approaches player
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFire < Time.time)
        {
            anim.SetTrigger("rangedAttack");
            nextFire = Time.time + fireRate;
        }
        else
        {
            //Idle
            anim.SetBool("moving", false);
        }
        */

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void updateBehavior()
    {
        
    }

    private void FacePlayer()
    {
        //Face the player
        tempScale = transform.localScale;

        if(transform.position.x > player.position.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }

    private void ShootPlayer()
    {
        //Creates a bullet the shoots the player

        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log("I was hit");

        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
