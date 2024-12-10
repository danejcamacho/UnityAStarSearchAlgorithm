using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class AStar : MonoBehaviour
{
    [SerializeField]
    float carSpeed = 1;
    [SerializeField]
    NodeCostManager NodeManager;
    List<Node> nodes = new();

    PriorityQueue frontier = new();
    List<Node> explored = new();
    private GameObject currentNodeObj; // this is for lerping
    GameObject startNodeObj;
    bool carIsMoving = false;
    int navIdx = 0;

    float timer = 0;

    private void Start() {
        foreach (var row in NodeManager.nodes)
        {
            foreach (var node in row)
            {
                nodes.Add(node.GetComponent<Node>());
            }
        }
        startNodeObj = nodes[0].gameObject;
        currentNodeObj = startNodeObj;
        transform.position = startNodeObj.transform.position;
        carIsMoving = false;
        AStarSearch();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (!carIsMoving && navIdx < explored.Count-1) {
            navIdx++;
            ChangeNode(explored[navIdx].gameObject);
            Debug.Log("Timer: " + timer);
        }
    }

    private bool AStarSearch() {
        frontier.Enqueue(nodes[0]);
        while(true) {
            if (frontier.GetCount() == 0) return false;
            Node currentNode = frontier.Dequeue();
            if(currentNode == nodes[^1]) {
                explored.Add(currentNode);
                return true;
            }
            explored.Add(currentNode);
            foreach (var neighbor in currentNode.neighbors)
            {
                Node neighborNode = neighbor.GetComponent<Node>();
                // if(!explored.Contains(neighborNode)) neighborNode.costFromStart = currentNode.costFromStart + neighborNode.GetRealPathCost();
                if (!frontier.p_queue.Contains(neighborNode) && !explored.Contains(neighborNode)) {
                    frontier.Enqueue(neighborNode);
                }
                else if(frontier.p_queue.Contains(neighborNode) && neighborNode.GetFinalCost() > currentNode.GetFinalCost()) {
                    currentNode = neighborNode;
                    // neighborNode = currentNode;
                }
            }
        }
    }

    public void ChangeNode(GameObject nodeToTravelTo){
        if(!carIsMoving){
            Debug.Log("Going to node: " + nodeToTravelTo.name + " Final Cost: " + nodeToTravelTo.GetComponent<Node>().GetFinalCost());
            StartCoroutine(TravelToNode(nodeToTravelTo));
        }
    }

    private IEnumerator TravelToNode(GameObject nodeToTravelTo){
        carIsMoving = true;
	    float time = 0;
        while (time < 1 ){
            time += Time.deltaTime * carSpeed;
            transform.position = Vector3.Lerp(currentNodeObj.transform.position, nodeToTravelTo.transform.position, time);
            yield return null;
        }

        currentNodeObj = nodeToTravelTo;
        carIsMoving = false;
        if (navIdx >= explored.Count) {
            Debug.Log("Timer: " + timer);
        }
    }
}

public class PriorityQueue {
    public List<Node> p_queue {get; private set;}= new();
    public Node Dequeue() {
        Node first = p_queue[0];
        p_queue.Remove(first);
        return first;
    }

    public void Enqueue(Node n) {
        p_queue.Add(n);
        SortByPriority();
    }
    public int GetCount() {
        return p_queue.Count;
    }

    private void SortByPriority() {
        p_queue.Sort(new MyComparer());
    }

    private class MyComparer : Comparer<Node> {
        public override int Compare(Node x, Node y)
        {
            return (x.costFromStart+x.GetHeuristic()).CompareTo(y.costFromStart+y.GetHeuristic());
        }
    }
}


