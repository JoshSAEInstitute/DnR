using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    private Transform player;

    //Faceplayer
    private SpriteRenderer sprite;
    private Vector2 tempScale;

    //Animation
    private Animator anim;

    //Shooting
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public float fireRate = 1;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            //Run Anim
            anim.SetBool("moving", true);

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


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void FacePlayer()
    {
        tempScale = transform.localScale;

        if (transform.position.x > player.position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

        transform.localScale = tempScale;

    }

    private void ShootPlayer()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
    }

}
