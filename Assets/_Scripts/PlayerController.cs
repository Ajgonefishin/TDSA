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
        Vector2 newPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        if(Input.GetKey(KeyCode.A)) {
            newPos.x -= speed;
            if (!renderer.flipX) {
                renderer.flipX = true; 
            }
        }

        if(Input.GetKey(KeyCode.W) && isGrounded()) 
                {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
                }

        if(Input.GetKey(KeyCode.D)) {
            newPos.x += speed;
            if (renderer.flipX) {
                renderer.flipX = false; 
            }
        }
        gameObject.transform.position = newPos;
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

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        Vector3 normal = other.GetContact(0).normal;
    //        if (normal == Vector3.up)
    //        {
    //            grounded = true;
    //        }


    //    }
    //}

    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        grounded = false;
    //    }
    //}

}