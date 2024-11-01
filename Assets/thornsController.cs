using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thornsController : MonoBehaviour
{
    Animator animator;
    //Rigidbody2D rb;
    private TrailRenderer tr;

    public GameObject currentCheckpoint;
    void Start()
    {
        // initialize components
        animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController targetableObject = collision.gameObject.GetComponent<PlayerController>();

        if (targetableObject != null)
        {

            targetableObject.killPlayerHelper();
        }

    }
}
