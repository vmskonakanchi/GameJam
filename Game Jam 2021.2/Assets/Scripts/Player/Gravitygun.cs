using UnityEngine;

public class Gravitygun : MonoBehaviour
{
    PlayerController player;
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }
    void Update()
    {

        HitWithRay();
    }
    void HitWithRay()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Input.mousePosition - mousePos).normalized;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit2d = Physics2D.Raycast(player.playerbulletFirePoint.position, direction * 10f);
            if (hit2d.collider == true && hit2d.collider != null)
            {
                Vector2 velocityDirX = this.transform.position - hit2d.transform.position;
                hit2d.rigidbody.velocity = velocityDirX * 10f;
            }
            Debug.Log(hit2d.transform.gameObject.tag);

        }

        Debug.DrawRay(player.playerbulletFirePoint.position, direction * 10f);

    }
}
