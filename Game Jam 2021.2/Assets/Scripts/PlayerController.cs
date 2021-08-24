using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    public GameObject bulletPrefab;
    public Transform playerbulletFirePoint;
    public Transform groundCheckPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    //[SerializeField] int playerHP = 100;
    [SerializeField] float launchForce;
    [SerializeField] bool isOnGround;
    [SerializeField] float groundHitRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Move();
        ChecKGround();
        Jump();
        Shoot();
    }
    void Shoot()
    {
        //Instataite projectiles depending upon ammo type 
        //play shoot sound
        //play shoot animation
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newbullet = Instantiate(bulletPrefab , playerbulletFirePoint.position , transform.rotation);
            newbullet.GetComponent<Rigidbody2D>().velocity =  mousePos * Vector2.right * transform.rotation *  launchForce;
        }
        
    }

    void Move()
    {
        //play move animation
        //play sound 
        //play particls if any
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = Vector2.right  * horizontalInput * moveSpeed * Time.deltaTime;  
        if(horizontalInput < 0)
        {
            sp.flipX = true;
        }   
    }

    void ChecKGround()
    {
        Collider2D groundHit = Physics2D.OverlapCircle(groundCheckPoint.position , groundHitRadius);
        if( groundHit == true)
        {
            isOnGround = true;
        }
        else if (groundHit == false)
        {
            isOnGround = false;
        }
    }
    void Jump()
    {
        //play jump sound
        //play jump animations
        //play particls if any
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            rb.velocity = Vector2.up * jumpSpeed * Time.deltaTime;
        }
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position , groundHitRadius);
    }
}
