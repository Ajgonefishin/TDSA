using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int jumpHeight;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    bool grounded;
    

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    // Update is called a fixed amount per second
    void FixedUpdate() {
        Vector2 newVelocity = new Vector2(0, 0);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Debug.Log(rb.velocity);
        Debug.Log("Horizontal: " + Input.GetAxis("Horizontal") + " Vertical: " + Input.GetAxis("Vertical"));

        Vector2 horizontalMovement = new Vector2(
            gameObject.transform.position.x + (Input.GetAxis("Horizontal") * Time.deltaTime * speed), 
            gameObject.transform.position.y
        );

        gameObject.transform.position = horizontalMovement; 
    
        if(Input.GetAxis("Vertical") > 0 && isGrounded()) {
            //newVelocity.y += jumpHeight;
            rb.AddForce(new Vector2(0, jumpHeight * speed));
        }

        if (Input.GetAxis("Horizontal") > 0 && renderer.flipX) renderer.flipX = false;
        if (Input.GetAxis("Horizontal") < 0 && !renderer.flipX) renderer.flipX = true;

        // if(Input.GetKey(KeyCode.A)) {
        //     newVelocity.x -= 1;
        //     rb.velocity = new Vector2(newVelocity.x * speed, rb.velocity.y);
        //     if (!renderer.flipX) {
        //         renderer.flipX = true; 
        //     }
        // }

        // if(Input.GetKey(KeyCode.D)) {
        //     newVelocity.x += 1;
        //     rb.velocity = new Vector2(newVelocity.x * speed, rb.velocity.y);
        //     if (renderer.flipX) {
        //         renderer.flipX = false; 
        //     }
        // }  

        //gameObject.transform.position += newPos * speed;
        //rb.AddForce(newPos*speed);
        //rb.velocity = rb.velocity + (newVelocity * speed);
    }   

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        // if (collision.gameObject.name == "Grid") {
        //     rb.velocity = new Vector2(0,0);
        // }
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