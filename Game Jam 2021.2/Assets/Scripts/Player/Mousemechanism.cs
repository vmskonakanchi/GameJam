using UnityEngine;
public class Mousemechanism : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 correctpos = transform.position;
        Vector2 Dirction = mousePos - correctpos;
        transform.right = Dirction;
    }
}