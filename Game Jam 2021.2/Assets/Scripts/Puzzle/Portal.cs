using UnityEngine;
public class Portal : MonoBehaviour
{
    Animator am;
    SpriteRenderer sp;
    SpriteRenderer sp1;


    private void Start()
    {
        GameObject ParentgameObject = this.gameObject;
        GameObject ChildGameObject = ParentgameObject.transform.GetChild(0).gameObject;
        sp = ChildGameObject.GetComponentInChildren<SpriteRenderer>();
        sp1 = GetComponent<SpriteRenderer>();
        am = GetComponent<Animator>();
        sp1.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && am.enabled == false)
        {
            sp1.enabled = true;
            sp.enabled = false;
            am.enabled = true;
            Debug.Log("Playing");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && am.enabled == true)
        {
            sp1.enabled = false;
            sp.enabled = true;
            am.enabled = false;
            Debug.Log("Not Playing");
        }
    }


}
