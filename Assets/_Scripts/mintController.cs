using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mintController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public float mintCooldown = 5f;
    private bool mintOn = true;

    void Start()
    {
        // initialize components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController targetableObject = collision.gameObject.GetComponent<PlayerController>();

        if (targetableObject.dashUnlocked == true && mintOn)
        {
            //sound effect!
            // refresh dash
            targetableObject.canDash = true;

            //disable mint leaf for mintCooldown amount of seconds
            StartCoroutine(mintLogic());

        }
        else {
            mintOn = false;
            animator.SetFloat("mintOnFake", 0);
        }


        IEnumerator mintLogic()
        {
            mintOn = false;
            animator.SetFloat("mintOnFake", 0);

            yield return new WaitForSeconds(mintCooldown);
            //sound effect!
            // re-enable mint leaf
            mintOn = true;
            animator.SetFloat("mintOnFake", 1);
        }
    }
}
