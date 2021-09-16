using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    Animator am;
    SpriteRenderer sp;
    SpriteRenderer sp1;
    PlayerController player;

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GameObject ParentgameObject = this.gameObject;
        GameObject ChildGameObject = ParentgameObject.transform.GetChild(0).gameObject;
        sp = ChildGameObject.GetComponentInChildren<SpriteRenderer>();
        sp1 = GetComponent<SpriteRenderer>();
        am = GetComponent<Animator>();
        sp1.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.hasKey == true)
        {
            if (collision.CompareTag("Player") && am.enabled == false)
            {
                sp1.enabled = true;
                sp.enabled = false;
                am.enabled = true;
                Debug.Log("Playing");
            }
        }
        if (!player.hasKey)
        {
            Debug.Log("Need key To unlock the door");
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (player.hasKey == true)
            {
                StartCoroutine(LoadScene());            
            }
        }
        
    }


}
