using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum MyEnum
{
    GroundOrPlatform,
    RightWall,
    Ceiling,
    LeftWall
};
public class Patrol : MonoBehaviour
{
    public MyEnum roboPlacement = new MyEnum();
    private Vector2 right;
    private Vector2 down;
    private Vector3 leftRotation;
    private Vector3 rightRotation;
    LayerMask layerMask;
    public Vector2 shootDistance;
    public float roboHp = 100;
    public float speed;
    private bool movingRight = true;
    private bool patrolState = true;
    private bool isShooting = false;
    public GameObject missile;
    public Transform firePoint;
    public Transform groundDetection;
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private Slider slider;
    private void Start()
    {
        slider = gameObject.GetComponentInChildren<Slider>();
        layerMask = LayerMask.GetMask("Obstacle");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player != null)
        {
            UpdateHealth();
            Die();
            PlaceRobo();
            CheckEdges();
        }
    }
    private void CheckEdges()
    {
        float playerDistanceX = Mathf.Abs(player.position.x - transform.position.x);
        float playerDistanceY = Mathf.Abs(player.position.y - transform.position.y);
        // The Raycast
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, layerMask);
        // If it hits the player
        if (hit.collider == null && !isShooting && playerDistanceX < shootDistance.x && playerDistanceY < shootDistance.y)
        {
            patrolState = false;
        }
        if (patrolState && !isShooting)
        {
            if (movingRight)
            {
                rb.velocity = speed * right;
            }
            else
            {
                rb.velocity = -speed * right;
            }
        }
        else if (!patrolState && !isShooting)
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(Shoot());
        }
        RaycastHit2D edgeCheck = Physics2D.Raycast(groundDetection.position, down, 0.5f);
        RaycastHit2D frontCheck = Physics2D.Raycast(groundDetection.position, right, 0.1f);
        if (!edgeCheck.collider || frontCheck)
        {
            if (movingRight)
            {
                transform.eulerAngles = leftRotation;
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = rightRotation;
                movingRight = true;
            }
        }
    }
    private void PlaceRobo()
    {
        switch(roboPlacement)
        {   
            case MyEnum.GroundOrPlatform:
                right = Vector2.right;
                down = Vector2.down;
                leftRotation = new Vector3(0, 180, 0);
                rightRotation = new Vector3(0, 0, 0);
                break;
            case MyEnum.RightWall:
                right = Vector2.up;
                down = Vector2.right;
                leftRotation = new Vector3(180, 0, 90);
                rightRotation = new Vector3(0, 0, 90);
                break;
            case MyEnum.Ceiling:
                right = Vector2.left;
                down = Vector2.up;
                leftRotation = new Vector3(0, 180, 180);
                rightRotation = new Vector3(0, 0, 180);
                break;
            case MyEnum.LeftWall:
                right = Vector2.down;
                down = Vector2.left;
                leftRotation = new Vector3(180, 0, 270);
                rightRotation = new Vector3(0, 0, 270);
                break;
            default:
                break;
        }
    }
    IEnumerator Shoot()
    {
        patrolState = false;
        isShooting = true;
        anim.Play("Robo Shoot");
        yield return new WaitForSeconds(1f);
        Instantiate(missile, firePoint.position, new Quaternion(right.x, right.y, 0, 0));
        patrolState = true;
        isShooting = false;
    }
    void UpdateHealth()
    {
        slider.value = roboHp;
    }
    void Die()
    {       
        if (roboHp == 0)
        {
            Destroy(gameObject);
            //Play Death Animation
        }
    }
    public void Damage()
    {
        roboHp -= 12.5f;
    }
}
