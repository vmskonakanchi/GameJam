using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sp;
    Animator am;
    Gravitygun gravitygun;
    public GameObject bulletPrefab;
    public Transform playerbulletFirePoint;
    public Transform groundCheckPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    //[SerializeField] int playerHP = 100;
    [SerializeField] float launchForce;
    [SerializeField] bool isOnGround;
    [SerializeField] float groundHitRadius;
    // bool isGravitygunon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        am = GetComponent<Animator>();
        gravitygun = gameObject.GetComponent <Gravitygun>();
    }
    void Update()
    {
        gravitygun.HitWithRay();
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
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(playerbulletFirePoint.position);
            Vector3 direction = (Input.mousePosition - mousePos).normalized;
            GameObject newbullet = Instantiate(bulletPrefab , playerbulletFirePoint.position , Quaternion.identity);
            newbullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * launchForce;
        }
        else
        {
            return;
        }

    }

    void Move()
    {
        //play move animation
        //play sound 
        //play particls if any
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerbulletFirePoint.rotation = transform.rotation;
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce (Vector2.left * moveSpeed * Time.deltaTime);
            sp.flipX = true;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce (Vector2.right * moveSpeed * Time.deltaTime);
            sp.flipX = false;
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
            rb.AddForce (jumpSpeed * Time.deltaTime * Vector2.up, ForceMode2D.Force);
        }
        else
        {
            return;
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position , groundHitRadius);
    }
}
