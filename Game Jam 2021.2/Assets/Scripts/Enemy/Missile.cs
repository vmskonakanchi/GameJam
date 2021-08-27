using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    float speed = 1;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Following()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        Vector3 deltaPosition = speed * dir * Time.deltaTime;
        rb.MovePosition(transform.position + deltaPosition);
    }

    void Explode()
    {

    }
}
