using UnityEngine;
using System.Collections.Generic;
using Pathfinding.BFS;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float boxSize;

    private LayerMask obstalceLayer;
    private Vector2Int gridSize;

    public Dictionary<Vector2, Node> nodes = new Dictionary<Vector2, Node>();

    private void Awake()
    {
        CreateGrid();
    }

    private void OnDrawGizmos()
    {
        DrawGrid();
    }

    private void CreateGrid()
    {
        gridSize.x = (int)GameObject.FindGameObjectWithTag("Ground").transform.lossyScale.x;
        gridSize.y = (int)GameObject.FindGameObjectWithTag("Ground").transform.lossyScale.y;
        for (int x = 0; x <= gridSize.x; x++)
        {
            for (int y = 0; y <= gridSize.y; y++)
            {
                Vector2 gridPoints = new Vector2(x, y);
                nodes.Add(gridPoints, new Node(gridPoints, true, true));
            }
        }
    }
    private void DrawGrid()
    {
        obstalceLayer = LayerMask.GetMask("Obstacle");
        Gizmos.color = Color.white;
        foreach (var node in nodes)
        {
            if (Physics2D.OverlapBox(node.Key, new Vector2(boxSize, boxSize), 0f, obstalceLayer))
            {
                Gizmos.color = Color.cyan;
            }
            else if (node.Value.isExplored)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawCube(node.Key, new Vector3(boxSize, boxSize));
        }
    }

    public Node GetNode(Vector2 coordinates)
    {
        if (nodes.ContainsKey(coordinates))
        {
            return nodes[coordinates];
        }
        return null;
    }
}