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

    //Basic Movements
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    //Dashing
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    //Animation
    private enum movementState { idle, running, jumping, fall }


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
        if (isDashing)
        {
            return;
        }


        dirX = Input.GetAxis("Horizontal");
        RB.velocity = new Vector2(dirX * moveSpeed, RB.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }



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
        }
        else if (RB.velocity.y < -0.0001f)
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = RB.gravityScale;
        RB.gravityScale = 0f;
        if (!sprite.flipX)
        {
            RB.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        else if (sprite.flipX)
        {
            RB.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        }
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        RB.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}