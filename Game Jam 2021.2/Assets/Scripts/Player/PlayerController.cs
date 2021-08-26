using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator am;
    Gravitygun gravitygun;
    [Header("Components Required")]
    public GameObject bulletPrefab;
    public Transform playerbulletFirePoint;
    public Transform groundCheckPoint;
    [Space(3f)]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    //[SerializeField] int playerHP = 100;
    [SerializeField] public float launchForce;
    [SerializeField] bool isOnGround;
    float groundHitRadius = 0.08f;
    // bool isGravitygunon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        gravitygun = gameObject.GetComponent<Gravitygun>();
    }
    void FixedUpdate()
    {
        Move();
        gravitygun.HitWithRay();
        Jump();
        ChecKGround();
        Shoot();
    }
    void Update()
    {
        PlayAnimations();
    }
    void Shoot()
    {
        //Instataite projectiles depending upon ammo type 
        //play shoot sound
        if (Input.GetMouseButtonDown(0))
        {
            am.SetTrigger("Shoot");
            GameObject newbullet = Instantiate(bulletPrefab, playerbulletFirePoint.position, Quaternion.identity);
            newbullet.GetComponent<Rigidbody2D>().velocity = playerbulletFirePoint.rotation * Vector2.right * launchForce;
        }
        else
        {
            am.SetBool("hasGun", false);
            return;
        }

    }

    void Move()
    {

        //play sound 
        //play particls if any
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = moveSpeed * Time.deltaTime * Vector2.left;
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = moveSpeed * Time.deltaTime * Vector2.right;
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    void ChecKGround()
    {
        Collider2D groundHit = Physics2D.OverlapCircle(groundCheckPoint.position, groundHitRadius);
        if (groundHit == true)
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
        //play particls if any
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            rb.velocity = jumpSpeed * Time.deltaTime * Vector2.up;
        }
        else
        {
            return;
        }

    }
    void PlayAnimations()
    {
        if (rb.velocity == Vector2.zero)
        {
            am.SetFloat("Speed", 1);
        }
        else if (rb.velocity.x > 0 && rb.velocity.y == 0)
        {
            am.SetFloat("Speed", 2);
        }
        else if (rb.velocity.x < 0 && rb.velocity.y == 0)
        {
            am.SetFloat("Speed", 2);
        }
        else if (rb.velocity.y > 0)
        {
            am.SetTrigger("Jump");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundHitRadius);
    }
}
