<<<<<<< HEAD:Game Jam 2021.2/Assets/Scripts/Enymy/EnemyAI_GC.cs
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
=======
using UnityEngine;

public class SuitGuyAI_GC : MonoBehaviour
{
    [SerializeField] private SuitGuyAI suitGuyAIScript;

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            suitGuyAIScript.GroundUncheck();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.IsTouchingLayers(9))
            suitGuyAIScript.GroundCheck();
    }
}
>>>>>>> 333388855e62cdf50dd40749d4348b714026a827:Game Jam 2021.2/Assets/Suit Guy/SuitGuyAI_GC.cs
