using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // variables for standard movement
    public float speed;
    public float jumpHeight;

    // variables for variable jump height
    public float jumpStartTime;
    public float jumpTime;
    public bool midJump;

    // variables to check if grounded
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    bool grounded;
    
    // variables for death and checkpoints
    public float bottomDeathBox;
    public GameObject currentCheckpoint;

    // variables for dash
    private bool isDashing;
    public bool canDash = true;
    public float dashVelocity;
    public float dashTime; // in seconds
    public float dashCooldown; // in seconds
    public bool dashUnlocked;

    // audio sources
    public AudioSource dashSound;
    public AudioSource deathSound;

    // gameObject components to be used across methods
    Animator animator;
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private TrailRenderer tr;
    

    // Start is called before the first frame update
    void Start() {
        // initialize components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (isDashing) return;

        //set isJumping to true if you're not grounded and vice versa, this handles animation
        animator.SetBool("isJumping", !isGrounded());
        animator.SetFloat("yVelocity", rb.velocity.y);

        //DASH
        if (Input.GetButtonDown("Fire3") && canDash && dashUnlocked) {
            tr.emitting = true; // enable dash trail
            StartCoroutine(Dash());
        }

        if (isGrounded() && !isDashing)
        {
            canDash = true;
        }

        if (Input.GetAxis("Vertical") < 0 && midJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight / 999);
        }
    }

    // Update is called a fixed amount per second
    void FixedUpdate() {
        if (isDashing) return;

        Debug.Log("is grounded: " + isGrounded());
        Debug.Log("Horizontal: " + Input.GetAxis("Horizontal") + " Vertical: " + Input.GetAxis("Vertical"));
        Debug.Log("transform.whatever : " + transform.localScale.x);

        // set vertical velocity (horizontal movement)
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));

        //JUMP
        if (Input.GetAxis("Vertical") > 0 && isGrounded()) {
            //if (Input.GetButtonDown("Vertical") && isGrounded()) {
            jumpTime = jumpStartTime;
            midJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetFloat("yVelocity", rb.velocity.y);

        ////jump pressure test
        //if (Input.GetAxis("Vertical") > 0 && midJump)
        //    {
        //        if (jumpTime > 0) {
        //            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        //            jumpTime -= Time.deltaTime;

        //        }
        //        else
        //        {
        //            midJump = false;

        //        }

        //    }

        //if (Input.GetAxis("Vertical") < 0)
        //    {
        //        midJump = false;
        //    }

        }

        // check if died
        if (gameObject.transform.position.y < bottomDeathBox) {
            killPlayer();
        }

        // flip sprite if necessary
        if (Input.GetAxis("Horizontal") > 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
        if (Input.GetAxis("Horizontal") < 0 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
    }   

    public bool isGrounded() {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        animator.SetBool("isJumping", !grounded);
    }

    public void killPlayer() {
        rb.velocity = new Vector2();
        gameObject.transform.position = currentCheckpoint.transform.position;
    }


    // coroutine for dashing. based on https://www.youtube.com/watch?v=2kFGmuPHiA0 with changes
    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        dashSound.Play();
        // disable gravity temporarily while the dash is active
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        // if flipX is true, we're pointing left; if flipX is false, we're pointing right
        float direction = 0;
        if (spriteRenderer.flipX) {
            direction = -1;
        } else {
            direction = 1;
        }
        // set velocity 
        rb.velocity = new Vector2(direction * dashVelocity, 0);
        // wait in this state until dashTime has elapsed
        yield return new WaitForSeconds(dashTime);
        // return to normal pre-dash state
        rb.gravityScale = gravity;
        isDashing = false;
        // wait until dashCooldown has elapsed until we can dash again
        yield return new WaitForSeconds(dashCooldown);
        tr.emitting = false; // disable dash trail
    }

}