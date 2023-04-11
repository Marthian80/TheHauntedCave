using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rb2d;
    BoxCollider2D bodyCollider;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }
        
    void Update()
    {
        Movement();
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipSprite();
    }

    void FlipSprite()
    {   
        transform.localScale = new Vector2(-Mathf.Sign(rb2d.velocity.x), 1f);        
    }

    void Movement()
    {
        rb2d.velocity = new Vector2(moveSpeed, 0f);
    }
}
