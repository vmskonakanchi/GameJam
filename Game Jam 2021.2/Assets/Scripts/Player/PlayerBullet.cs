using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    PlayerController player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.bulletCount += 2;
        }
        if (col.GetComponent<SuitGuyAI>() != null)
        {
            if (col.GetComponent<SuitGuyAI>() == true)
            {
                col.GetComponent<Animator>().Play("Suit Guy Hurt");
                col.GetComponent<SuitGuyAI>().Damage();
                Destroy(gameObject);
            }
        }
        if (col.GetComponent<Bullet>() == true || col.GetComponent<Missile>() == true)
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else if (col.GetComponent<Patrol>() != null)
        {
         if (col.GetComponent<Patrol>() == true)
            {
                col.GetComponent<Animator>().Play("Robo Hurt");
                col.GetComponent<Patrol>().Damage();
                Destroy(gameObject);
            }
        }
        else if (col.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }
    }
}
