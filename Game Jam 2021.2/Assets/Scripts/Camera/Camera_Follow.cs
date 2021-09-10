using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    GameObject player;
    Transform target;
    [SerializeField] Vector3 offset;
    Vector3 smoothPos;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) target = player.transform;
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            smoothPos = Vector3.Lerp(transform.position, (target.position + offset), 1f);
            transform.position = smoothPos;
        }
    }
}
