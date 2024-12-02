using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Node : MonoBehaviour
{
    private float heuristic;
    public GameObject goalNode;
    public List<GameObject> neighbors;
    public float costToTravelToNode = 1;

    private void Awake(){
        heuristic = Vector3.Distance(goalNode.transform.position, transform.position);
    }
    void Start(){
        //initialize to avoid error
        goalNode = gameObject;
    }

    void OnDrawGizmos(){
        //Draw the node connections
        foreach(GameObject neighbor in neighbors){
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    void Update(){

    }
    /// <summary>
    /// This is the getter for the heuristic value of the node. The Euclidean Distance is used for this.
    /// </summary>
    /// <returns>The Euclidean distance to the goal</returns>
    public float GetHeuristic(){
        return heuristic;
    }

    public float GetCost(){
        return costToTravelToNode;
    }
}
