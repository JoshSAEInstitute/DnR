using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Sprites and Animation
    private Rigidbody2D RB;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    //Health
    //private Health myHealth;
    //private int myCurrentHealth;

    //Check if can jump
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableEnemy;

    //Basic Movements
    public Vector2 dirX;
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

    //Input Systems
    public PlayerInputActions pcs;
    private InputAction move, exit, jump, dash;

    private void Awake()
    {
        pcs = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = pcs.Player.Move;
        move.Enable();

        exit = pcs.Player.Exit;
        exit.Enable();
        exit.performed += Exit;

        jump = pcs.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        
        dash = pcs.Player.Dash;
        dash.Enable();
        dash.performed += ButtonDash;
        
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        dash.Disable();
        exit.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Gets all components
        RB = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //myHealth = GetComponent<Health>();
        //myCurrentHealth = myHealth.currentHealth;

        /*
        //Sets HP
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        */
    }

    // Update is called once per frame
    private void Update()
    {

        if (isDashing)
        {
            return;
        }


        //dirX = Input.GetAxis("Horizontal");
        //RB.velocity = new Vector2(dirX * moveSpeed, RB.velocity.y);

        
        dirX = move.ReadValue<Vector2>();
        RB.velocity = new Vector2(dirX.x * moveSpeed, RB.velocity.y);

        /*
        if(myInput.x > 0)
        {

        }
        else if(myInput.x < 0)
        {

        }
        */

        /*
        Vector2 dirX = move.ReadValue<Vector2>();
        transform.Translate(new Vector2(dirX.x, dirX.y) * Time.deltaTime * moveSpeed);
        */

        //Vector2 dirX = move.ReadValue<Vector2>();
        //RB.velocity = new Vector2(dirX.x * moveSpeed, 0);



        /*
        if ((Input.GetButtonDown("Jump") && isGrounded()) || (Input.GetButtonDown("Jump") && isOnEnemy()))
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }
        */
        /*
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        */


        /*
        //To check if the healthbar works
        if(Input.GetKeyDown(KeyCode.L))
        {
            myHealth.TakeDamage(1);
        }
        */




        //Calls the animation
        updateAnimation();

    }

    private void updateAnimation()
    {
        //Here animations are handled

        movementState state;

        if (dirX.x > 0f)
        {
            state = movementState.running;
            sprite.flipX = false;
        }
        else if (dirX.x < 0f)
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

    public bool isGrounded()
    {
        /*
         * Creates a box around the player with the same shape of the box collider,
         * offset by 0.1f which will overlap to the ground checking weather the player is grounded or not
         * then if we're overlapping with the jumpable ground, we can jump from it
        */

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);

    }

    public bool isOnEnemy()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableEnemy);
    }

    public IEnumerator Dash()
    {
        //Check if player can dash
        canDash = false;
        isDashing = true;

        //Removes gravity so that the player stais in air when dashing in air
        float originalGravity = RB.gravityScale;
        RB.gravityScale = 0f;

        //Flips the sprite when dashing left or right
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

        //Re-applies gravity
        RB.gravityScale = originalGravity;

        //Cooldown for dashing again
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded() || isOnEnemy())
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce);
        }
    }

    private void ButtonDash(InputAction.CallbackContext context)
    {
        if(canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private void Exit(InputAction.CallbackContext context)
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    /*
    void TakeDamage(int damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
    }
    */
}