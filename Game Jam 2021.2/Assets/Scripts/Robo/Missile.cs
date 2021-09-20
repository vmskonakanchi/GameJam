using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private GameObject enymy;
    private SuitGuyAI enemy;
    public GameObject explosionFx;
    private GameObject player;
    private Transform playerPos;
    private PlayerController playerController;

    private float speed = 8f;
    private float timer = 2f;
    public int missileDamage = 10;

    private Collider2D collider1;
    private Collider2D collider2;
    private Collider2D collider3;

    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        enemy = FindObjectOfType<SuitGuyAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerPos = player.transform;
        if (player != null) playerController = player.GetComponent<PlayerController>();
        collider1 = this.GetComponent<Collider2D>();
        if (enymy != null) collider2 = enemy.GetComponent<Collider2D>();
        collider3 = FindObjectOfType<Patrol>().GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (player != null) StartCoroutine(Timer());
    }

    private void FixedUpdate()
    {
        if (collider3 != null) Physics2D.IgnoreCollision(collider1, collider3);
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
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }

    //Missile follows the player and calls the rotate function
    private void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        RotateTowards(playerPos.position);
    }

    private void Explode()
    {
        GameObject collisionFx = Instantiate(explosionFx, transform.position, transform.rotation);
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