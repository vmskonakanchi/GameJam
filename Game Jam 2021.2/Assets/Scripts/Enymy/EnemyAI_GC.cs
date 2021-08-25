using UnityEngine;

public class EnemyAI_GC : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAIScript;

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            enemyAIScript.GroundUncheck();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            enemyAIScript.GroundCheck();
    }
}
