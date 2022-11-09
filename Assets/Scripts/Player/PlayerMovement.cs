using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D RB;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private bool doubleJump;

    private enum movementState { idle, running, jumping, fall}
    

    // Start is called before the first frame update
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        //Disable movement while dashing
        //if (isDashing)
        //{
          //  return;
       // }

        //Moving left or right

        dirX = Input.GetAxis("Horizontal");
        RB.velocity = new Vector2 (dirX * moveSpeed, RB.velocity.y);

        //Jumping
        if (isGrounded() && Input.GetButton("Jump"))
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);

            doubleJump = false;
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (!isGrounded() && !doubleJump)
            {
                anim.SetBool("doubleJump", true);
                RB.velocity = new Vector2(RB.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
            
        }

        //Do dashing
        //if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        //{
            //StartCoroutine(Dash());
        //}



        //Calls the animation

        updateAnimation();

       
    }

    private void updateAnimation()
    {
        //Here animations are handled

        movementState state;

        if (dirX > 0f)
        {
            state = movementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = movementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = movementState.idle;
        }

        if (RB.velocity.y > .0001f)
        {
            state = movementState.jumping;
        }else if (RB.velocity.y < -0.0001f)
        {
            state = movementState.fall;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        /*
         * Creates a box around the player with the same shape of the box collider,
         * offset by 0.1f which will overlap to the ground checking weather the player is grounded or not
         * then if we're overlapping with the jumpable ground, we can jump from it
        */

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
