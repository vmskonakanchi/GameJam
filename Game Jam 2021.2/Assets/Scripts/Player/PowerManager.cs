using UnityEngine;
using System.Collections;
public class PowerManager : MonoBehaviour
{
    Color speed = Color.red;
    Color jump = Color.blue;
    Color shoot = Color.green;
    public GameObject speedpickup;
    public GameObject jumppickup;
    public GameObject shootpickup;
    PlayerController player;
    SpriteRenderer sp;
    [SerializeField] private float powerinfluencer = 10;
    [SerializeField] private float Timer;
    private bool canPickup;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        sp = GetComponent<SpriteRenderer>();
        canPickup = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            if (other.gameObject.GetComponent<SpriteRenderer>().color == speed && canPickup == true)
            {
                StartCoroutine(Speed());
            }
            else if (other.gameObject.GetComponent<SpriteRenderer>().color == jump && canPickup == true)
            {
                StartCoroutine(Jump());
            }
            else if (other.gameObject.GetComponent<SpriteRenderer>().color == shoot && canPickup == true)
            {
                StartCoroutine(Shoot());
            }
            Destroy(other.gameObject);
        }
    }
    private IEnumerator Speed()
    {
        sp.color = speed;
        canPickup = false;
        player.moveSpeed += powerinfluencer;
        yield return new WaitForSeconds(Timer);
        player.moveSpeed -= powerinfluencer;
        canPickup = true;
        sp.color = Color.white;
    }
    private IEnumerator Jump()
    {
        sp.color = jump;
        canPickup = false;
        player.jumpSpeed += powerinfluencer;
        yield return new WaitForSeconds(Timer);
        player.jumpSpeed -= powerinfluencer;
        canPickup = true;
        sp.color = Color.white;
    }
    private IEnumerator Shoot()
    {
        sp.color = shoot;
        canPickup = false;
        player.deservedAmmo += Mathf.RoundToInt(powerinfluencer);
        yield return new WaitForSeconds(Timer);
        player.deservedAmmo -= Mathf.RoundToInt(powerinfluencer);
        canPickup = true;
        sp.color = Color.white;
    }
}