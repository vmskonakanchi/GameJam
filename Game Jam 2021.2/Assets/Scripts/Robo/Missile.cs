using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject enymy;
    SuitGuyAI enemy;
    public GameObject explosionFx;
    GameObject player;
    Transform playerPos;
    PlayerController playerController;

    float speed = 8f;
    Rigidbody2D rb;
    float timer = 2f;
    public int missileDamage = 10;

    Collider2D collider1;
    Collider2D collider2;
    void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        enymy = GameObject.FindGameObjectWithTag("Enemy");
        if(enymy != null) enemy = FindObjectOfType<SuitGuyAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerPos = player.transform;
        if (player != null) playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        collider1 = this.GetComponent<Collider2D>();
        if(enymy != null)collider2 = enemy.GetComponent<Collider2D>();

    }

    private void Update()
    {
        if (player != null) StartCoroutine(Timer());

    }
    void FixedUpdate()
    {
        if (enymy != null) Physics2D.IgnoreCollision(collider1, collider2);
        if (player != null) Follow();
    }

    //Collision check
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player != null)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<Animator>().Play("Edward Hurt");
                Debug.Log("Hit By Robo Missile");
                Destroy(gameObject);
                playerController.playerHP -= missileDamage;
                Explode();
            }
            else if (collision.gameObject.GetComponent<Patrol>() == null)
            {
                Explode();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Animator>().SetBool("Hurt", false);
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
        GameObject collisionFx = Instantiate(explosionFx, transform.position, transform.rotation);
        collisionFx.transform.rotation = transform.rotation;
        Destroy(collisionFx, 0.3f);
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
