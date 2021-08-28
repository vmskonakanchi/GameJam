using UnityEngine;

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

    public float speed;
    private bool movingRight = true;

    public Transform groundDetection;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (roboPlacement == MyEnum.GroundOrPlatform)
        {
            right = Vector2.right;
            down = Vector2.down;
            leftRotation = new Vector3(0, 180, 0);
            rightRotation = new Vector3(0, 0, 0);
        }
        if (roboPlacement == MyEnum.RightWall)
        {
            right = Vector2.up;
            down = Vector2.right;
            leftRotation = new Vector3(180, 0, 90);
            rightRotation = new Vector3(0, 0, 90);
        }
        if (roboPlacement == MyEnum.Ceiling)
        {
            right = Vector2.left;
            down = Vector2.up;
            leftRotation = new Vector3(0, 180, 180);
            rightRotation = new Vector3(0, 0, 180);
        }
        if (roboPlacement == MyEnum.LeftWall)
        {
            right = Vector2.down;
            down = Vector2.left;
            leftRotation = new Vector3(180, 0, 270);
            rightRotation = new Vector3(0, 0, 270);
        }

        if (movingRight)
        {
            rb.velocity = speed * right;
        }
        else
        {
            rb.velocity = -speed * right;
        }

        RaycastHit2D edgeCheck = Physics2D.Raycast(groundDetection.position, down, 0.5f);
        RaycastHit2D frontCheck = Physics2D.Raycast(groundDetection.position, right, 0.1f);
        if (!edgeCheck.collider || frontCheck)
        {
            if(movingRight)
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
}
