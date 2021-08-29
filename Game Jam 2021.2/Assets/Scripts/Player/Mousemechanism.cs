using UnityEngine;
    public class Mousemechanism : MonoBehaviour
    {
    PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
    void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 correctPos = mousePos - Input.mousePosition;
            transform.position = player.playerbulletFirePoint.position - correctPos;
            transform.rotation = player.playerbulletFirePoint.rotation;
        }
    }