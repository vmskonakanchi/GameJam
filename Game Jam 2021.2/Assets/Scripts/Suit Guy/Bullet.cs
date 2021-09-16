using UnityEngine;

public class Bullet : MonoBehaviour
{ 
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
