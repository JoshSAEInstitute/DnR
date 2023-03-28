using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_Shoot;

public class Shooting_State : MonoBehaviour
{
    //Sight
    private Transform player;
    private Movement_State movementState;

    //Animation
    private Animator anim;

    //Shooting
    public float shootingRange;
    public float fireRate = 1;
    private float nextFire;

    public GameObject bullet;
    public GameObject bulletParent;

    //Shooting Behaviour
    public enum shootingBehaviour { idle, shoot }
    public shootingBehaviour movingState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        movementState= this.GetComponent<Movement_State>();
    }

    private void Update()
    {
        //Calculates distance between this and the player
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        switch (movingState)
        {
            case shootingBehaviour.idle:

                anim.SetBool("moving", false);
                //Debug.Log("I'm idling");
                //Check if can shoot, this also checks if the enemy is currently moving or not as it can obly shoot when is not moving.
                if (nextFire < Time.time && movementState.movingState == Movement_State.movingBehaviour.idle && distanceFromPlayer <= shootingRange)
                {
                    movingState = shootingBehaviour.shoot;
                }
                break;

            case shootingBehaviour.shoot:
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
                    movingState = shootingBehaviour.idle;
                }

                break;
        }
    }
    private void ShootPlayer()
    {
        //Creates a bullet the shoots the player, this called in the animation
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
    }
}
