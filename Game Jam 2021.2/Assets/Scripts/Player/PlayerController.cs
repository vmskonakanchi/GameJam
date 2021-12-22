using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerController : MonoBehaviour
{
    private AudioManager audioManager;
    private GameObject[] enemy;
    private Rigidbody2D rb;
    private Animator am;
    private SpriteRenderer sp;
    public Button retryButton;
    public Slider slider;
    public TextMeshProUGUI needkeyText;
    public TextMeshProUGUI scoreText;
    private Gravitygun gravitygun;
    [Header("Components Required")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlsh;
    public Transform playerbulletFirePoint;
    public Transform groundCheckPoint;
    public Transform LadderCheck;
    [Space(3f)]
    public float moveSpeed = 6;
    public float jumpSpeed = 8;
    [SerializeField] private float laddercheckDist;
    [HideInInspector]
    public int deservedAmmo = 10;
    public int playerHP = 100;
    public int bulletCount = 15;
    [SerializeField] public float launchForce;
    [SerializeField] private bool isOnGround;
    private float groundHitRadius = 0.08f;
    public bool hasgravityGun;
    public bool hasEneryGun = false;
    public Transform WeaponHolder;
    public GameObject GravityGun;
    public GameObject EnergyGun;
    private Animator energyGun_am;
    private Mousemechanism mouse;
    public bool canFire;
    public bool hasKey = false;
    private LayerMask whatisLadder;
    private bool isA = false;
    private bool isD = false;
    private bool isW = false;
    private bool isSpace = false;
    private bool isMouse_0 = false;
    private void Awake()
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
        Die();
        Jump();
        gravitygun.HitWithRay();
        gravitygun.DragObject();
        gravitygun.StopObjectFrontOfPlayer();
        GetInput();
        PlaySound();
        CountAmmo();
        Shoot();
        ChangeGuns();
        PlayAnimations();
        UpdateUI();
        AddBulletsOnEnemyDeath();
        ROtateGuns();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(LadderCheck.position, Vector2.up);
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundHitRadius);
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.GetComponent<Bullet>() != null || col.collider.GetComponent<Missile>() != null)
        {
            if (col.collider.GetComponent<Bullet>() == true || col.collider.GetComponent<Missile>() == true)
            {
                audioManager.PlaySound("playerHurt");
            }
        }
    }
    private void GetComponents()
    {
        Cursor.visible = false;
        mouse = FindObjectOfType<Mousemechanism>();
        audioManager = FindObjectOfType<AudioManager>();
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        whatisLadder = LayerMask.GetMask("Ladder");
        energyGun_am = EnergyGun.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        gravitygun = GetComponent<Gravitygun>();
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
    private void ROtateGuns()
    {
        if (WeaponHolder.eulerAngles.z < 90f && WeaponHolder.eulerAngles.z > -90f)
        {
            GravityGun.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            EnergyGun.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (WeaponHolder.eulerAngles.z > 90f && WeaponHolder.eulerAngles.z > 0f)
        {
            GravityGun.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
            EnergyGun.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
        }
    }
    private void Move()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (isA)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            am.SetFloat("Speed", 2);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (isD)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            sp.flipX = false;
            am.SetFloat("Speed", 2);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            am.SetFloat("Speed", 1);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void Jump()
    {
        if (isSpace && isOnGround)
        {
            isSpace = false;
            isOnGround = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    private void ClimbLadders()
    {
        RaycastHit2D ladderHit = Physics2D.Raycast(LadderCheck.position, Vector2.up, laddercheckDist, whatisLadder);
        if (ladderHit.collider)
        {
            if (isW)
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
        if (EnergyGun.gameObject.activeSelf) if (canFire) if (isMouse_0) audioManager.PlaySound("playerShootE");
        if (isOnGround) if (isSpace) audioManager.PlaySound("playerJump");
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
        if (isOnGround) if (isSpace) am.SetTrigger("Jump");
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
        if (playerHP <= 0)
        {
            audioManager.PlaySound("playerDeath");
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
                        audioManager.PlaySound("playerAddAmmo");
                        bulletCount += deservedAmmo / 2;
                    }
                }
                if (g.GetComponent<Patrol>() != null)
                {
                    if (g.GetComponent<Patrol>().roboHp == 0)
                    {
                        audioManager.PlaySound("playerAddAmmo");
                        bulletCount += deservedAmmo;
                    }
                }
            }
        }
    }
    private void ShowRetrybuttons()
    {
        Cursor.visible = true;
        retryButton.gameObject.SetActive(true);
    }
    public void RetryButton()
    {
        Scene presentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(presentScene.name);
    }
}