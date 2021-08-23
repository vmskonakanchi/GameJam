using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] int playerHP = 100;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Shoot();
    }
    void Update()
    {
        Shoot();
    }

    void Jump()
    {
        //play jump sound
        //play jump animations
        //play particls if any
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce (Vector2.up * jumpSpeed * Time.deltaTime , ForceMode2D.Impulse);
        }
    }   
   void Shoot()
    {
        //Instataite projectiles
    }

    void Move()
    {
        //play move animation
        //play sound 
        //play particls if any
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = Vector2.right  * horizontalInput * moveSpeed * Time.deltaTime;  
        if(horizontalInput == -1)
        {
            sp.flipX = true;
        }   
    }
}
