﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float bounceHeight;
    AudioSource bounceAudio;

    void Start()
    {
        // initialize components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bounceAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController targetableObject = collision.gameObject.GetComponent<PlayerController>();
        bounceAudio.Play();
        targetableObject.rb.AddForce(new Vector2(0f, bounceHeight), ForceMode2D.Impulse);
        // refresh dash
        targetableObject.canDash = true;

    }
}
