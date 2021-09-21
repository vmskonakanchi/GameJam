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
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<Animator>().Play("Edward Hurt");
            col.collider.GetComponent<PlayerController>().BulletDamage();
            Destroy(gameObject);
        }
        else if (col.collider.IsTouchingLayers(9))
        {
            Destroy(gameObject);
        }      
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.collider.CompareTag("Player"))
        {
            col.collider.GetComponent<Animator>().SetBool("Hurt", false);
        }
    }
}
