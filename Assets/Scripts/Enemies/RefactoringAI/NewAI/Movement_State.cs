using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_State : MonoBehaviour
{
    //Enemy speed
    public float speed;
    private float originalSpeed;
    public float runAwaySpeed;

    //Enemy line of sight
    public float lineOfSight;
    public float tooCloseRange;
    public float goodRange;
    private Transform player;

    //Faceplayer
    private Vector3 tempScale;

    //Animation
    private Animator anim;

    //Health
    public int maxHP;
    public int currentHP;

    //Behaviour
    public enum movingBehaviour { idle, approach, leave }
    public movingBehaviour movingState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        currentHP = maxHP;

        originalSpeed = speed;
    }

    void Update()
    {
        FacePlayer();
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        switch (movingState)
        {
            case movingBehaviour.idle:

                anim.SetBool("moving", false);

                //--- to Approach
                if (distanceFromPlayer < lineOfSight && distanceFromPlayer > goodRange)
                {
                    movingState = movingBehaviour.approach;
                }

                //--- to Leave
                if (distanceFromPlayer < tooCloseRange && distanceFromPlayer < goodRange)
                {
                    movingState = movingBehaviour.leave;
                }

                break;

            case movingBehaviour.approach:
                //Run Anim
                speed = originalSpeed;
                anim.SetBool("moving", true);

                //Object approaches player
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                //--- to Idle
                if (distanceFromPlayer > lineOfSight || distanceFromPlayer < goodRange)
                {

                    movingState = movingBehaviour.idle;
                }
                //--- to Leave
                if (distanceFromPlayer <= tooCloseRange)
                {
                    movingState = movingBehaviour.leave;
                }
                break;

            case movingBehaviour.leave:
                speed = runAwaySpeed;
                anim.SetBool("moving", true);

                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime * -1);
                //--- to Idle
                if (distanceFromPlayer > goodRange)
                {
                    movingState = movingBehaviour.idle;
                }
                //--- to Approach
                if (distanceFromPlayer > tooCloseRange)
                {
                    movingState = movingBehaviour.approach;
                }
                break;
        }

    }
    private void FacePlayer()
    {
        //Face the player
        tempScale = transform.localScale;

        if (transform.position.x > player.position.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }

    public void EnemyTakeDamage(int damage)
    {
        //Enemy takes damage
        currentHP -= damage;

        Debug.Log("I was hit");

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, tooCloseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, goodRange);
    }
}
