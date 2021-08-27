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
        GetComponents();
    }

    private void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        gravitygun = gameObject.GetComponent<Gravitygun>();
    }

    void FixedUpdate()
    {
        gravitygun.HitWithRay();
        
    }
    void Update()
    {
        Jump();
        Move();
        Shoot();
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
        //play particles if any
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (Input.GetKey(KeyCode.A))
        {
            am.SetFloat("Speed", 2);
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            am.SetFloat("Speed", 2);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            am.SetFloat("Speed", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void Jump()
    {
        //play jump sound
        //play particls if any
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            isOnGround = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
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
        else if (rb.velocity == Vector2.right || rb.velocity == Vector2.left)
        {
            am.SetFloat("Speed", 2);
        }
        else if (rb.velocity ==  Vector2.up)
        {
            am.SetTrigger("Jump");
        }
    }
    public void BulletDamage()
    {
        Debug.Log("Hit with bullet by enemy");
    }
   public void GroundCheck()
    {
        isOnGround = true;
        Debug.Log("Hit Ground");
    }
    public void GroundUncheck()
    {
        isOnGround = false;
        Debug.Log("Not Hit Ground");
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundHitRadius);
    }
}
