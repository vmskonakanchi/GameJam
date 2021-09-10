using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject player;
    Transform playerPos;
    PlayerController playerController;

    float speed = 8f;
    Rigidbody2D rb;
    float timer = 2f;
    public int missileDamage = 10;
    void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerPos = player.transform;
        if (player != null) playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null) StartCoroutine(Timer());

    }
    void FixedUpdate()
    {
        if (player != null) Follow();

    }

    //Collision check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player != null)
        {

            if (collision.collider.tag == "Player")
            {
                Debug.Log("Hit By Robo Missile");
                Destroy(gameObject);
                playerController.playerHP -= missileDamage;
            }
            else if (collision.gameObject.GetComponent<Patrol>() == null)
            {
                Explode();
            }
        }
    }


    //Timer for missile to explode
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }

    //Function that the missile follows the player and calls the rotate function
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        RotateTowards(playerPos.position);
    }
    void Explode()
    {
        Destroy(gameObject);
    }
    //Rotate the missile towards the player
    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
