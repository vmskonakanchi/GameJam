using UnityEngine;

public class SuitGuyAI_GC : MonoBehaviour
{
    [SerializeField] private SuitGuyAI suitGuyAIScript;

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            suitGuyAIScript.GroundUncheck();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            suitGuyAIScript.GroundCheck();
    }
}
