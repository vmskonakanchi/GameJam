using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public AudioSource jumpSound;
    Rigidbody2D rb;
    Animator am;
    SpriteRenderer sp;
    public Button retryButton;
    public Slider slider;
    public TextMeshProUGUI scoreText;
    Gravitygun gravitygun;
    [Header("Components Required")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlsh;
    public Transform playerbulletFirePoint;
    public Transform groundCheckPoint;
    public Transform LadderCheck;
    [Space(3f)]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float laddercheckDist;
    public int playerHP = 100;
    [SerializeField] public float launchForce;
    [SerializeField] bool isOnGround;
    float groundHitRadius = 0.08f;
    public bool hasgravityGun;
    public bool hasEneryGun = false;
    public GameObject GravityGun;
    public GameObject EnergyGun;
    Animator energyGun_am;
    Ammo ammo;
    public bool canFire;
    public bool hasKey = false;
    LayerMask whatisLadder;

    bool isA;
    bool isD;
    bool isW;
    bool isSpace;
    bool isMouse_0;
    void Start()
    {
        GetComponents();
    }
    private void GetComponents()
    {
        whatisLadder = LayerMask.GetMask("Ladder");
        ammo = FindObjectOfType<Ammo>();
        energyGun_am = EnergyGun.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        gravitygun = gameObject.GetComponent<Gravitygun>();
        sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        ClimbLadders();
        Move();
        Jump();
        CheckGround();
        gravitygun.HitWithRay();
    }
    void Update()
    {       
        GetInput();
        PlaySound();
        CountAmmo();
        Shoot();
        ChangeGuns();
        PlayAnimations();
        Die();
        UpdateUI();
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.A)) isA = true; else isA = false;
        if (Input.GetKey(KeyCode.D)) isD = true; else isD = false;
        if (Input.GetKey(KeyCode.W)) isW = true; else isW = false;
        if (Input.GetKeyDown(KeyCode.Space)) isSpace = true; else isSpace = false;
        if (Input.GetMouseButtonDown(0)) isMouse_0 = true; else isMouse_0 = false;
    }
    void ChangeGuns()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasEneryGun = true;
            hasgravityGun = false;
            EnergyGun.gameObject.SetActive(true);
            GravityGun.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hasEneryGun = false;
            hasgravityGun = true;
            GravityGun.gameObject.SetActive(true);
            EnergyGun.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hasEneryGun = false;
            hasgravityGun = false;
            GravityGun.gameObject.SetActive(false);
            EnergyGun.gameObject.SetActive(false);
        }
    }
    void Shoot()
    {   
        if (hasEneryGun == true)
        {
            if (isMouse_0 == true)
            {
                if (canFire == true)
                {
                    ammo.bulletCount--;
                    energyGun_am.SetBool("shoot", true);
                    GameObject flsh = Instantiate(muzzleFlsh, playerbulletFirePoint.position, playerbulletFirePoint.rotation);
                    Destroy(flsh, 0.05f);
                    GameObject newbullet = Instantiate(bulletPrefab, playerbulletFirePoint.position, playerbulletFirePoint.rotation);
                    newbullet.GetComponent<Rigidbody2D>().velocity = playerbulletFirePoint.rotation * Vector2.right * launchForce;
                }
            }
            else energyGun_am.SetBool("shoot", false);
        }
    }
    void CheckGround()
    {
        Collider2D groundInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundHitRadius);
        if (groundInfo == true) isOnGround = true;
        else isOnGround = false;
    }

    void Move()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (isA == true)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            am.SetFloat("Speed", 2);
            sp.flipX = true;
        }
        else if (isD == true)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            sp.flipX = false;
            am.SetFloat("Speed", 2);
        }
        else
        {
            am.SetFloat("Speed", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void Jump()
    {
        if (isSpace == true && isOnGround == true)
        {
            isOnGround = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    void ClimbLadders()
    {
        RaycastHit2D ladderHit = Physics2D.Raycast(LadderCheck.position, Vector2.up, laddercheckDist, whatisLadder);
        if (ladderHit.collider == true)
        {
            Debug.Log("Press W To Climb Ladder");         
            if (isW == true)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0, moveSpeed);
            }
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
    void CountAmmo()
    {
        if (ammo.bulletCount > 0)
        {
            canFire = true;
        }
        else if (ammo.bulletCount == 0) canFire = false;
    }
    void PlaySound()
    {
        if (isSpace == true)
        {
            jumpSound.Play();
        }
       // Debug.Log(jumpSound.isPlaying);
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
        else if (rb.velocity == Vector2.up)
        {
            am.SetTrigger("Jump");            
        }
       
    }
    public void BulletDamage()
    {
        playerHP -= 5;
        Debug.Log("Hit with bullet by enemy");
    }
    void UpdateUI()
    {
        scoreText.text = ammo.bulletCount.ToString();
        slider.value = playerHP;
    }
    void Die()
    {
        if (playerHP == 0)
        {
            am.Play("Edward Death");
            Debug.Log("Player Died");
            Destroy(gameObject, 0.3f);
            ShowRetrybuttons();
        }
    }
    void ShowRetrybuttons()
    {
        retryButton.gameObject.SetActive(true);

    }
    public void RetryButton()
    {
        Scene presentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(presentScene.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(LadderCheck.position, Vector2.up);
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundHitRadius);
    }
}
