using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D RB;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum movementState { idle, running, jumping, fall}
    

    // Start is called before the first frame update
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        RB.velocity = new Vector2 (dirX * moveSpeed, RB.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }

        updateAnimation();
        //Calls the animation
       
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
}
