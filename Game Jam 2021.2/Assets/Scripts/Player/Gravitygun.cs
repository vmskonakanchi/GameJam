using UnityEngine;

public class Gravitygun : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float raycastDist;
    RaycastHit2D hit2d;
    Vector3 mousePos;
    Vector3 correctPos;
    Animator gravityGun_am;
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        if (player != null) gravityGun_am = player.GravityGun.GetComponent<Animator>();
    }

    public void HitWithRay()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        correctPos = (mousePos - transform.position).normalized;

        if (Input.GetMouseButton(1) && player.GravityGun.activeSelf == true)
        {
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
        else
        {
            if (player.hasgravityGun == true)
                gravityGun_am.SetBool("shoot", false);
        }
        Debug.DrawRay(player.playerbulletFirePoint.position, correctPos, Color.green);
    }
}
