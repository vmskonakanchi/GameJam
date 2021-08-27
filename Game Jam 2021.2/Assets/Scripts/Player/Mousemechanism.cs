using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Mousemechanism : MonoBehaviour
    { 
        void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 arrowPos = transform.position;
            Vector2 arrowDirection = mousePos - arrowPos;
            transform.right = arrowDirection;
        }
    }
}