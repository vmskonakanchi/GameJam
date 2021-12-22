using UnityEngine;
namespace Pathfinding.BFS
{
    [System.Serializable]
    public class Node
    {
        public Vector2 coordinates;
        public bool isWalkable;
        public bool isExplored;
        public Node(Vector2 _coordinates, bool _isWalkable, bool _isExplored)
        {
            coordinates = _coordinates;
            isWalkable = _isWalkable;
            isExplored = _isExplored;
        }
    }
}