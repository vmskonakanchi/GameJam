using UnityEngine;
public class Mousemechanism : MonoBehaviour
{
    PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 correctpos = (mousePos - (Vector2)player.playerbulletFirePoint.position).normalized;
        transform.right = correctpos;
    }
}