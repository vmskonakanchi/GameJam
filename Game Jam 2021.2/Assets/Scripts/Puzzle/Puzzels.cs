using UnityEngine;
public class Puzzels : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject Trigger;
    Rigidbody2D trigrb;
    private void Start()
    {
        trigrb = Trigger.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
        {
            rb.mass -= collision.collider.attachedRigidbody.mass;
            trigrb.position += new Vector2 (0,rb.position.y);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
        {
            rb.mass += collision.collider.attachedRigidbody.mass;
            trigrb.position -= new Vector2(0, rb.position.y);
        }
    }
}
