using UnityEngine;

public class SuitGuyAI_GC : MonoBehaviour
{
    [SerializeField] private SuitGuyAI suitGuyAIScript;

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
        {
            suitGuyAIScript.GroundUncheck();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
        {
            suitGuyAIScript.GroundCheck();
        }
    }
}
