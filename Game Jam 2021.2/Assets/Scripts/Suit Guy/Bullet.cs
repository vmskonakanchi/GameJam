using UnityEngine;
public class Bullet : MonoBehaviour
{
    Collider2D bullet;
    Collider2D robo;
    private void Start()
    {
        bullet = this.GetComponent<Collider2D>();
        robo = FindObjectOfType<Patrol>().GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        Physics2D.IgnoreCollision(bullet, robo);
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Animator>().Play("Edward Hurt");
            col.GetComponent<PlayerController>().BulletDamage();
            Destroy(gameObject);
        }
        else if (col.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }      
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<Animator>().SetBool("Hurt", false);
        }
    }
}
