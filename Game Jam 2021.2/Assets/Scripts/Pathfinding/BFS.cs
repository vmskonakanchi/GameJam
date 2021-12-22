using UnityEngine;
using System.Collections.Generic;
using Pathfinding.BFS;

public class BFS : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    private Node startnode;
    private Node endnode;
    private Node currentsearchNode;

    private Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

    private GridManager manager;

    private Queue<Node> frontier = new Queue<Node>();

    private Dictionary<Vector2, Node> reached = new Dictionary<Vector2, Node>();

    private void Start()
    {
        manager = FindObjectOfType<GridManager>();

        startPos = new Vector2Int((int)GameObject.FindGameObjectWithTag("Player").transform.position.x, (int)GameObject.FindGameObjectWithTag("Player").transform.position.y);
        endPos = new Vector2Int((int)GameObject.FindGameObjectWithTag("Target").transform.position.x, (int)GameObject.FindGameObjectWithTag("Target").transform.position.y);

        startnode = new Node(startPos, true, true);
        endnode = new Node(endPos, true, true);

        FindPath();
    }

    private void FindPath()
    {
        bool isRunning = true;
        frontier.Enqueue(startnode);
        reached.Add(startPos, startnode);
        while (frontier.Count > 0 && isRunning)
        {
            currentsearchNode = frontier.Dequeue();
            currentsearchNode.isExplored = true;
            ExploreNeighbours();
            if (currentsearchNode.coordinates == endPos)
            {
                isRunning = false;
            }

        }
    }

    private void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();
        foreach (var dir in directions)
        {
            Vector2 _sideCoordinates = currentsearchNode.coordinates + dir;
            if (manager.nodes.ContainsKey(_sideCoordinates))
            {
                neighbours.Add(manager.nodes[_sideCoordinates]);
            }
        }
        foreach (var neighbour in neighbours)
        {
            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                reached.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }
}