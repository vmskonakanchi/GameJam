using UnityEngine;

public class Gravitygun : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float raycastDist;
    RaycastHit2D hit2d;
    Vector3 mousePos;
    Vector3 correctpos;



    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();

    }
    void Update()
    {


    }

    public void HitWithRay()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        correctpos = (mousePos - player.playerbulletFirePoint.position).normalized;
        if (Input.GetMouseButton(1))
        {
            hit2d = Physics2D.Raycast(player.playerbulletFirePoint.position, correctpos, raycastDist);
            if (hit2d.rigidbody != null)

            {
                if (hit2d.rigidbody.gameObject.tag != "Player")
                {
                    hit2d.rigidbody.gravityScale = -1;
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (hit2d.rigidbody.gameObject.tag != "Player")
                    {
                        hit2d.rigidbody.gravityScale = 1;
                    }
                }
            }
                  Debug.DrawRay(player.playerbulletFirePoint.position, correctpos, Color.green);
        }

                else return;
            }  
}
