using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject[] enemy;
    public AudioSource jumpSound;
    private Rigidbody2D rb;
    private Animator am;
    private SpriteRenderer sp;
    public Button retryButton;
    public Slider slider;
    public TextMeshProUGUI scoreText;
    private Gravitygun gravitygun;

    [Header("Components Required")]
    public GameObject bulletPrefab;

    public GameObject muzzleFlsh;
    public Transform playerbulletFirePoint;
    public Transform groundCheckPoint;
    public Transform LadderCheck;

    [Space(3f)]
    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float laddercheckDist;
    public int playerHP = 100;
    public int bulletCount = 15;
    [SerializeField] public float launchForce;
    [SerializeField] private bool isOnGround;
    private float groundHitRadius = 0.08f;
    public bool hasgravityGun;
    public bool hasEneryGun = false;
    public GameObject GravityGun;
    public GameObject EnergyGun;
    private Animator energyGun_am;
    public bool canFire;
    public bool hasKey = false;
    private LayerMask whatisLadder;

    private bool isA = false;
    private bool isD = false;
    private bool isW = false;
    private bool isSpace = false;
    private bool isMouse_0 = false;

    private void Start()
    {
        GetComponents();
    }

    private void FixedUpdate()
    {
        ClimbLadders();
        Move();
        CheckGround();
    }

    private void Update()
    {
        Jump();
        gravitygun.HitWithRay();
        GetInput();
        PlaySound();
        CountAmmo();
        Shoot();
        ChangeGuns();
        PlayAnimations();
        Die();
        UpdateUI();
        AddBulletsOnEnemyDeath();
    }

    private void GetComponents()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        whatisLadder = LayerMask.GetMask("Ladder");
        energyGun_am = EnergyGun.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        gravitygun = gameObject.GetComponent<Gravitygun>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void GetInput()
    {
        isA = Input.GetKey(KeyCode.A);
        isD = Input.GetKey(KeyCode.D);
        isW = Input.GetKey(KeyCode.W);
        isSpace = Input.GetKeyDown(KeyCode.Space);
        isMouse_0 = Input.GetMouseButtonDown(0);
    }

    private void ChangeGuns()
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

    private void Shoot()
    {
        if (hasEneryGun == true)
        {
            if (isMouse_0 == true)
            {
                if (canFire == true)
                {
                    bulletCount--;
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

    private void CheckGround()
    {
        Collider2D groundInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundHitRadius);
        if (groundInfo == true) isOnGround = true;
        else isOnGround = false;
    }

    private void Move()
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

    private void Jump()
    {
        if (isSpace && isOnGround == true)
        {
            isSpace = false;
            isOnGround = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    private void ClimbLadders()
    {
        RaycastHit2D ladderHit = Physics2D.Raycast(LadderCheck.position, Vector2.up, laddercheckDist, whatisLadder);
        if (ladderHit.collider == true)
        {
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

    private void CountAmmo()
    {
        if (bulletCount > 0)
        {
            canFire = true;
        }
        else if (bulletCount == 0) canFire = false;
    }

    private void PlaySound()
    {
        if (isSpace == true)
        {
            jumpSound.Play();
        }
    }

    private void PlayAnimations()
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
    }

    private void UpdateUI()
    {
        scoreText.text = bulletCount.ToString();
        slider.value = playerHP;
    }

    private void Die()
    {
        if (playerHP == 0)
        {
            am.Play("Edward Death");
            Destroy(gameObject, 0.3f);
            ShowRetrybuttons();
        }
    }

    private void AddBulletsOnEnemyDeath()
    {
        if (enemy == null) return;
        foreach (GameObject g in enemy)
        {
            if (g != null)
            {
                if (g.GetComponent<SuitGuyAI>() != null)
                {
                    if (g.GetComponent<SuitGuyAI>().enemyHP == 0)
                    {
                        bulletCount += 5;
                    }
                }
                if (g.GetComponent<Patrol>() != null)
                {
                    if (g.GetComponent<Patrol>().roboHp == 0)
                    {
                        bulletCount += 10;
                    }
                }
            }
        }
    }

    private void ShowRetrybuttons()
    {
        retryButton.gameObject.SetActive(true);
    }

    public void RetryButton()
    { 
        Scene presentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(presentScene.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(LadderCheck.position, Vector2.up);
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundHitRadius);
    }
}