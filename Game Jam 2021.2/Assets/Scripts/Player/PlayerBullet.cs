using System.Collections;
using UnityEngine;
public class PlayerBullet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    { 
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<SuitGuyAI>().Damage();
            Destroy(gameObject);
        }
        else if (col.CompareTag("RoboEnemy"))
        {
            col.GetComponent<Patrol>().Damage();
            Destroy(gameObject);
        }
        else if (col.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }
    }
}
