using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Ammo ammo;
    private void Start()
    {
        ammo = FindObjectOfType<Ammo>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ammo.bulletCount += 2;
        }
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Animator>().Play("Suit Guy Hurt");
            col.GetComponent<SuitGuyAI>().Damage();
            Destroy(gameObject);           
        }
        if (col.GetComponent<Bullet>() == true || col.GetComponent<Missile>() == true)
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else if (col.CompareTag("RoboEnemy"))
        {
            col.GetComponent<Animator>().Play("Robo Hurt");
            col.GetComponent<Patrol>().Damage();
            Destroy(gameObject);
        }
        else if (col.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }
    }
}
