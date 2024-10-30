using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpHeight;
    public float bottomDeathBox;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public GameObject currentCheckpoint;

    public float dashDistance;
    public float dashTime; // in seconds
    //private Vector2 dashDirection;
    private float dashDirection = 1f;
    public bool dashUnlock = false;
    private bool isDashing = false;
    private bool canDash;
    private TrailRenderer tr;

    bool grounded;
    Animator animator;
    

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update() {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        //set isJumping to true if you're not grounded and vice versa, this handles animation
        animator.SetBool("isJumping", !isGrounded());
        //and same for dash
        animator.SetBool("isDashingAni", isDashing);

        animator.SetFloat("yVelocity", rb.velocity.y);

        //DASH 

        if ((Input.GetAxis("Dash") > 0) && dashUnlock && canDash && !isDashing)
        {
            isDashing = true;
            canDash = false;
            StartCoroutine(Dash(dashDirection));
            //tr.emitting = true; // enable dash trail
        }



            ////DASH beta

            //    //dashDirection = new Vector2(Input.GetAxis("Horizontal"), rb.velocity.y);
            //    //dashDirection = new Vector2(x:Input.GetAxisRaw("Horizontal"), y:Input.GetAxisRaw("Vertical"));
            //    dashDirection = new Vector2(1f, 5f);
            //    //dashDirection = new Vector2(transform.localScale.x, y:0);
            //    //dashDirection = new Vector2();
            //    StartCoroutine(stopDash());
            //}

            //if(isDashing)
            //{
            //    //rb.velocity = dashDirection.normalized * dashVelocity;
            //    rb.velocity = dashDirection.normalized * dashVelocity;
            //    return; //important idk why
            //}

            if (isGrounded())
        {
            canDash = true;
        }

    }

    // Update is called a fixed amount per second
    void FixedUpdate() {
        Vector2 newVelocity = new Vector2(0, 0);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Debug.Log("is grounded: " + isGrounded());
        Debug.Log("Horizontal: " + Input.GetAxis("Horizontal") + " Vertical: " + Input.GetAxis("Vertical"));

        // set vertical velocity (horizontal movement)
        if (!isDashing) 
        { 
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        }

        //JUMP
        if (Input.GetAxis("Vertical") > 0 && isGrounded() && !isDashing) {
        //if (Input.GetButtonDown("Vertical") && isGrounded()) {
            //newVelocity.y += jumpHeight;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetFloat("yVelocity", rb.velocity.y);
        }

        // check if died
        if (gameObject.transform.position.y < bottomDeathBox) {
            killPlayer();
        }

        // flip sprite if necessary
        if (Input.GetAxis("Horizontal") > 0 && renderer.flipX)
        {
            renderer.flipX = false;
            dashDirection = 1f;
        }
        if (Input.GetAxis("Horizontal") < 0 && !renderer.flipX)
        {
            renderer.flipX = true;
            dashDirection = -1f;
        }
    }   


    //private IEnumerator stopDash()
    //{
    //    yield return new WaitForSeconds(dashTime);
    //    //disable trail
    //    //tr.emitting = false;
    //    isDashing = false;
    //}


    public bool isGrounded() {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) {
            return true;
        }
        else {
            return false;
        }
    }

    IEnumerator Dash(float direction)
    {
        
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        //rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        // alright dash is over wrap it up 
        //disable trail
        //tr.emitting = false;
        isDashing = false;
        rb.gravityScale = gravity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        animator.SetBool("isJumping", !grounded);
        //animator.SetBool("isJumping", !grounded);
        // Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        // if (collision.gameObject.name == "Grid") {
        //     rb.velocity = new Vector2(0,0);
        // }
    }

    void killPlayer() {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2();
        gameObject.transform.position = currentCheckpoint.transform.position;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        Vector3 normal = other.GetContact(0).normal;
    //        if (normal == Vector3.up)
    //        {
    //            grounded = true;
    //        }
    //    }
    // }

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        grounded = false;
    //    }
    //}

}