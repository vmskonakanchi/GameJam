using UnityEngine;
public class Puzzels : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject Trigger;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.AddForceAtPosition(Vector2.down * 10, collision.transform.position);
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
        Trigger.transform.Translate(Vector2.up * 10);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {     
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
            Trigger.transform.Translate(Vector2.down * 10);
    }
}
