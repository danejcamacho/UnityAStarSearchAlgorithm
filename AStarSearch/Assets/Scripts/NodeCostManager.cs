using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NodeCostManager : MonoBehaviour
{
    public GameObject baseNode;

    [SerializeField]
    [Min(1)]
    int numNodesWide;
    [SerializeField]
    [Min(1)]
    int numNodesTall;
    int totalNodes = 2; // including Start and Goal node

    GameObject startNode;
    GameObject goalNode;

    public List<List<GameObject>> nodes {get; private set;} = new();

    float gridWidth;
    float gridHeight;

    float xDistBetweenNodes; // dist between node in x-axis
    float zDistBetweenNodes; // dist between node in z-axis

    private void Awake() {
        totalNodes = numNodesTall * numNodesWide;

        startNode = GameObject.FindWithTag("Start");
        goalNode = GameObject.FindWithTag("Goal");
        
        gridWidth = goalNode.transform.position.x - startNode.transform.position.x;
        gridHeight = goalNode.transform.position.z - startNode.transform.position.z;

        xDistBetweenNodes = gridWidth / (float)numNodesWide;
        zDistBetweenNodes = gridHeight / (float)numNodesTall;

        // Create 2D array of nodes
        for (int y = 0; y <= numNodesTall; y++)
        {
            List<GameObject> row = new();
            for (int x = 0; x <= numNodesWide; x++)
            {
                if (x == 0 && y == 0) row.Add(startNode);
                else if (x == numNodesTall && y == numNodesWide) row.Add(goalNode);
                else {
                    Vector3 pos = startNode.transform.position;
                    pos.x += xDistBetweenNodes * x;
                    pos.y = 0f;
                    pos.z += zDistBetweenNodes * y;
                    GameObject temp = Instantiate(baseNode, pos, quaternion.identity, transform);
                    temp.name = $"Node({x},{y})";
                    row.Add(temp);
                }
            }
            nodes.Add(row);
        }

        // Update Nodes neighbors
        for (int y = 0; y < nodes.Count; y++)
        {
            for (int x = 0; x < nodes[y].Count; x++)
            {
                AddAdjacentNeighbor(x,y);
                if (x == 0 && y == 0) continue;
                nodes[y][x].GetComponent<Node>().pathCost = xDistBetweenNodes;
                nodes[y][x].GetComponent<Node>().costFromStart = xDistBetweenNodes;
            }
        }
    }

    void AddAdjacentNeighbor(int x, int y) {
        Node node = nodes[y][x].GetComponent<Node>();
        //Top row
        if (CheckPosInBounds(x-1,y-1)) node.neighbors.Add(nodes[y-1][x-1]);
        if (CheckPosInBounds(x,y-1)) node.neighbors.Add(nodes[y-1][x]);
        if (CheckPosInBounds(x+1,y-1)) node.neighbors.Add(nodes[y-1][x+1]);
        // Middle row
        if (CheckPosInBounds(x-1,y)) node.neighbors.Add(nodes[y][x-1]);
        if (CheckPosInBounds(x+1,y)) node.neighbors.Add(nodes[y][x+1]);
        // Bot Row
        if (CheckPosInBounds(x-1,y+1)) node.neighbors.Add(nodes[y+1][x-1]);
        if (CheckPosInBounds(x,y+1)) node.neighbors.Add(nodes[y+1][x]);
        if (CheckPosInBounds(x+1,y+1)) node.neighbors.Add(nodes[y+1][x+1]);
    }

    bool CheckPosInBounds(int x, int y) {
        if (x < 0 || x >= nodes[0].Count) return false;
        if (y < 0 || y >= nodes[0].Count) return false;
        return true;
    }
}
