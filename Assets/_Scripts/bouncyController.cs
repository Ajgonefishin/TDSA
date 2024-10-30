using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float bounceHeight;

    void Start()
    {
        // initialize components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController targetableObject = collision.gameObject.GetComponent<PlayerController>();
        targetableObject.rb.AddForce(new Vector2(0f, bounceHeight), ForceMode2D.Impulse);
        // refresh dash
        targetableObject.canDash = true;

    }
}
