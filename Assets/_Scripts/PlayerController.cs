using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int jumpHeight;

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

        if(Input.GetKey(KeyCode.W)) {
            if (rb.velocity.y == 0) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
            }
        }

        if(Input.GetKey(KeyCode.D)) {
            newPos.x += speed;
            if (renderer.flipX) {
                renderer.flipX = false; 
            }
        }
        gameObject.transform.position = newPos;
    }

}