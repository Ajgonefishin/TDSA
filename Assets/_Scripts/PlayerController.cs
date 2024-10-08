using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    // Update is called a fixed amount per second
    void FixedUpdate() {
        Vector2 newPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if(Input.GetKey(KeyCode.A)) {
            newPos.x -= speed;
            if (!renderer.flipX) {
                renderer.flipX = true; 
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