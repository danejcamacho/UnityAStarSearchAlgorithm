using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Node : MonoBehaviour
{
    private float heuristic;
    public GameObject goalNode;
    public List<GameObject> neighbors;
    [Tooltip("Cost to travel to from adjacent nodes")]
    public float pathCost = 0;
    public float costFromStart = 0;
    private SphereCollider sphereCollider;

    // Modifies the cost based on terrain
    float surfaceCostMod = 0;

    private void Awake(){
        goalNode = GameObject.FindGameObjectWithTag("Goal");
        heuristic = Vector3.Distance(goalNode.transform.position, transform.position);
    }

    void Start(){
        //initialize to avoid error
        goalNode = gameObject;
        // Debug.Log(gameObject.name + ": " + GetRealPathCost());
    }

    void OnDrawGizmos(){
        //Draw the node connections
        foreach(GameObject neighbor in neighbors){
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    /// <summary>
    /// This is the getter for the heuristic value of the node. The Euclidean Distance is used for this.
    /// </summary>
    /// <returns>The Euclidean distance to the goal</returns>
    public float GetHeuristic(){
        return heuristic;
    }

    public float GetRealPathCost(){
        return pathCost * surfaceCostMod;
    }

    public float GetFinalCost(){
        return costFromStart + heuristic;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Environment")) return;
        surfaceCostMod = other.GetComponent<SurfaceCost>().Cost;
    }
}
