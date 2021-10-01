using UnityEngine;
using UnityEngine.Tilemaps;

public class Gravitygun : MonoBehaviour
{
    private AudioManager audioManager;
    private PlayerController player;
    [SerializeField] private float raycastDist;
    [SerializeField] public float dragDist;
    [SerializeField] private float dragSpeed;
    [SerializeField] public float stopradius;
    [SerializeField] private float hitSpeed;
    private RaycastHit2D hit2d;
    private RaycastHit2D draghit;
    private Vector3 mousePos;
    private Vector3 correctpos;
    private Animator gravityGun_am;
    private bool isMouse_1 = false;
    private bool isMouse_Hold = false;
    private bool isMouse_2 = false;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = gameObject.GetComponent<PlayerController>();
        if (player != null) gravityGun_am = player.GravityGun.GetComponent<Animator>();
    }

    private void Update()
    {
        isMouse_2 = Input.GetMouseButtonDown(0);
        isMouse_1 = Input.GetMouseButtonDown(1);
        isMouse_Hold = Input.GetMouseButton(1);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        correctpos = (mousePos - this.transform.position).normalized;
    }

    public void StopObjectFrontOfPlayer()
    {
        Collider2D obj;
        obj = Physics2D.OverlapCircle(player.playerbulletFirePoint.position, stopradius);
        if (!obj) return;
        if (obj && !obj.CompareTag("Player") && obj.GetComponent<Tilemap>() == null && player.GravityGun.activeSelf)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (!rb) return;
            if (rb)
            {
                if (isMouse_Hold)
                {
                    rb.velocity = Vector2.zero;
                }
                if (isMouse_2)
                {
                    rb.AddForceAtPosition(correctpos * hitSpeed, obj.transform.position, ForceMode2D.Force);
                    audioManager.PlaySound("throw");
                }
            }
        }
    }

    public void DragObject()
    {
        if (isMouse_Hold && player.GravityGun.activeSelf)
        {
            draghit = Physics2D.Raycast(player.playerbulletFirePoint.position, correctpos, dragDist);
            if (draghit.rigidbody == null) return;
            if (draghit.rigidbody != null)
            {
                bool isMissle = draghit.rigidbody.gameObject.name == "MissileParent";
                bool isBullet = draghit.rigidbody.gameObject.name == "EnemyBullet";
                bool isPuzzle = draghit.rigidbody.gameObject.tag == "Puzzle";
                bool isObject = draghit.rigidbody.gameObject.tag == "Object";
                bool isPlayer = draghit.rigidbody.gameObject.tag == "Player";

                bool isTagged = isMissle && isBullet && isPuzzle && isPlayer;

                bool isSuit = draghit.rigidbody.GetComponent<SuitGuyAI>() == null;
                bool isRobo = draghit.rigidbody.GetComponent<Patrol>() == null;

                bool isnotEnemies = isSuit && isRobo;

                if (isnotEnemies && !isTagged && isObject)
                {
                    Vector2 xPos = this.transform.position - draghit.rigidbody.transform.position;
                    draghit.rigidbody.AddForce(xPos * dragSpeed);
                    draghit.rigidbody.transform.rotation = player.playerbulletFirePoint.rotation;
                }
            }
        }
    }

    public void HitWithRay()
    {
        if (isMouse_1 && player.GravityGun.activeSelf == true)
        {
            hit2d = Physics2D.Raycast(player.playerbulletFirePoint.position, correctpos, raycastDist);
            audioManager.PlaySound("playerShootG");
            gravityGun_am.SetBool("shoot", true);
            player.hasgravityGun = true;
            if (hit2d.rigidbody == null) return;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bool isTags()
                {
                    if (hit2d.rigidbody.gameObject.tag == "Player") return true;
                    if (hit2d.rigidbody.gameObject.tag == "Puzzle") return true;
                    if (hit2d.rigidbody.gameObject.tag == "Object") return true;
                    if (hit2d.collider.gameObject.GetComponent<Tilemap>() == null) return true;
                    return false;
                }
                if (isTags())
                {
                    hit2d.rigidbody.gravityScale = -1;
                }
            }
        }
        else if (player.hasgravityGun == true)
        {
            gravityGun_am.SetBool("shoot", false);
        }
        Debug.DrawRay(player.playerbulletFirePoint.position, correctpos, Color.green);
    }
}