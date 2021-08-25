using UnityEngine;

public class Gravitygun : MonoBehaviour
{
    PlayerController player;
    public LineRenderer ray;
    [SerializeField] float pullingForce;
    
   
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();      
      
    }
    void Update()
    {
        
        ShowRay();
        
    }
    void ShowRay()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            ray.enabled = true;
        }
        else if (Input.GetMouseButton(1))
        {
            ray.SetPosition(0 , player.playerbulletFirePoint.position);
            ray.SetPosition(1 , mousePos);
        }
        if(Input.GetMouseButtonUp(1))
        {
            ray.enabled = false;
        }
    }
    public void HitWithRay()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(1))
        {
            RaycastHit2D hit2d = Physics2D.Raycast(player.playerbulletFirePoint.position, mousePos , 10f);
            if (hit2d.collider == null)
                return;
            else
            {
                Vector2 velocityDirX = this.transform.position - hit2d.transform.position;
                hit2d.rigidbody.AddForce (velocityDirX * pullingForce);
            }  
            Debug.Log(hit2d.transform.gameObject.tag);
        }
            //Debug.DrawRay(player.playerbulletFirePoint.position, mousePos , Color.white);
    }
}
