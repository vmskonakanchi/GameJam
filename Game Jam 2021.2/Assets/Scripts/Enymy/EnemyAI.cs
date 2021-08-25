using System.Collections;
using UnityEngine;

// The enemy will go to the position where he last saw the Player, and he will only shoot if the Player is directly in front of him, and within a certain distance
//There are 2 states: the Chase state and Shooting State

// For the EnemyAI and EnemyAI_GC scripts to work, there needs to be a player with the tag "Player", and obstacles with the ninth layer which should be called "Obstacle"
// Missing Jump/Falling Animation

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    public Vector2 chase_Range;
    public Vector2 shooting_Range;
    private bool chase_State;
    private bool shooting_State;

    public float shootingInterval;
    private bool canShoot = true;
    private bool isShooting = false;

    public float speed;
    private bool isGrounded = true;

    private float targetPosition;
    private int movingDirection;

    LayerMask layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        targetPosition = transform.position.x;
    }

    private void Update()
    {
        // If not in shooting state, plsy animations
        if (!shooting_State)
        {
            // When no movement is happening
            if (rb.velocity == Vector2.zero)
            {
                anim.Play("Enemy Idle");
            }

            // When Y movement is happening
            else if (rb.velocity.y != 0)
            {
                // No animation for Jumping/Falling yet
                anim.Play("Enemy Jump");
            }

            // When X movement is happening
            else if (rb.velocity.x != 0 && rb.velocity.y == 0)
            {
                anim.Play("Enemy Run");
            }
        }
    }

    private void FixedUpdate()
    {
        float playerDistanceX = Mathf.Abs(player.position.x - transform.position.x);
        float playerDistanceY = Mathf.Abs(player.position.y - transform.position.y);

        //Change States, but only when not in middle of shooting
        if (!isShooting)
        {
            if (playerDistanceX < chase_Range.x && playerDistanceX > shooting_Range.x && playerDistanceY < chase_Range.y || playerDistanceY > shooting_Range.y)
            {
                chase_State = true;
                shooting_State = false;
            }

            else if (playerDistanceX < shooting_Range.x && playerDistanceY < shooting_Range.y)
            {
                chase_State = false;
                shooting_State = true;
            }

            else
            {
                chase_State = false;
                shooting_State = false;
            }
        }

        // The Raycast
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, layerMask);

        // If it hits the player
        if (hit.collider != null)
        {
            Debug.Log("Not Spotted");
        }
        else
        {
            // Set a destination
            targetPosition = player.position.x;
            Debug.Log("Spotted");
        }

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

        // When ready to shoot, shoot
        if (shooting_State)
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }
        }

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

        // When chasing
        if (chase_State)
        {
            // Define moving direction
            if (targetPosition - transform.position.x > 0)
            {
                movingDirection = 1;
                sr.flipX = false;
            }
            else if (targetPosition - transform.position.x < 0)
            {
                movingDirection = -1;
                sr.flipX = true;
            }
        }
        // When shooting
        else if (shooting_State)
        {
            // Define moving direction
            if (player.position.x - transform.position.x > 0)
            {
                movingDirection = 1;
                sr.flipX = false;
            }
            else if (player.position.x - transform.position.x < 0)
            {
                movingDirection = -1;
                sr.flipX = true;
            }
        }

        // -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

        if (!shooting_State)
        {
            // Move enemy on the X axis in direction to the last spot the player was seen, only if grounded
            if (isGrounded)
            {
                rb.velocity = new Vector2(speed * movingDirection, rb.velocity.y);
            }
        }

        // Stop movement when close to target position to avoid gittering
        if (Mathf.Abs(targetPosition - transform.position.x) < 0.1f) rb.velocity = new Vector2(0, rb.velocity.y);
    }

    // Groundcheck from EnemyAI_GC script
    public void GroundCheck()
    {
        isGrounded = true;
    }
    public void GroundUncheck()
    {
        isGrounded = false;
    }

    // Coroutine with shoot code
    private IEnumerator Shoot()
    {
        anim.Play("Enemy Shooting");
        Debug.Log("Shoot");
        yield return new WaitForSeconds(shootingInterval);
        canShoot = true;
    }
}
