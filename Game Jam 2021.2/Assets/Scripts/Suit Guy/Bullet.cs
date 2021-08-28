using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().BulletDamage();
            Destroy(gameObject);
        }
        else if(col.CompareTag("Enemy"))
        {
            col.GetComponent<SuitGuyAI>().Damage();
        }
        else if (col.CompareTag("RoboEnemy"))
        {
            col.GetComponent<Patrol>().Damage();
        }
        else if (col.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }
    }
}
