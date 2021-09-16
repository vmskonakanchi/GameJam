using UnityEngine;
public class Key : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.collider.GetComponent<PlayerController>().hasKey = true;
        }
    }
}
