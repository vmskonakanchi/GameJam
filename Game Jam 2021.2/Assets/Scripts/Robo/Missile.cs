using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;

    float speed = 8;
    Rigidbody2D rb;

    float timer = 4;




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        
        
    }

    private void Update()
    {
        Timer();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();

        if(collision.collider.tag == "Player")
        {
            //Damage or Kill the Player
        }
    }

    void Timer()
    {
        if (Time.time > timer)
        {
            Explode();
        }
    }


    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        RotateTowards(player.position);
    }

    void Explode()
    {
        Destroy(this.gameObject);
    }







    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
