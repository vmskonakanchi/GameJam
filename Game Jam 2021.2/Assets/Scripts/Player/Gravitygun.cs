using UnityEngine;

public class Gravitygun : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private float raycastDist;
    private RaycastHit2D hit2d;
    private Vector3 mousePos;
    private Vector3 correctPos;
    private Animator gravityGun_am;
    bool isMouse_1 = false;

    private void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        if (player != null) gravityGun_am = player.GravityGun.GetComponent<Animator>();
    }
    private void Update()
    {
        isMouse_1 = Input.GetMouseButtonDown(1);
    }

    public void HitWithRay()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        correctPos = (mousePos - transform.position).normalized;

        if (isMouse_1 && player.GravityGun.activeSelf == true)
        {
            player.lasershootSound.Play();
            gravityGun_am.SetBool("shoot", true);
            player.hasgravityGun = true;
            hit2d = Physics2D.Raycast(player.playerbulletFirePoint.position, correctPos, raycastDist);
            if (hit2d.rigidbody != null)
            {
                if (hit2d.rigidbody.gameObject.tag != "Player" && hit2d.rigidbody.gameObject.tag != "Puzzle")
                {
                    hit2d.rigidbody.gravityScale = -1;
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (hit2d.rigidbody.gameObject.tag != "Player" && hit2d.rigidbody.gameObject.tag != "Puzzle")
                    {
                        hit2d.rigidbody.gravityScale = 1;
                    }
                }
            }
        }
        else if (player.hasgravityGun == true) gravityGun_am.SetBool("shoot", false);
        Debug.DrawRay(player.playerbulletFirePoint.position, correctPos, Color.green);
    }
}