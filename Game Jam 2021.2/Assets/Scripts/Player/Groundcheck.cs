using UnityEngine;

public class Groundcheck : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            playerController.GroundUncheck();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            playerController.GroundCheck();
    }
}
