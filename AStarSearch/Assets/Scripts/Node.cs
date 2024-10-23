using UnityEngine;

public class Node : MonoBehaviour
{
    private float heuristic;
    public GameObject goalNode;
    public GameObject[] neighbors;

    private void Awake(){
        heuristic = Vector3.Distance(goalNode.transform.position, transform.position);
    }
    void Start(){

    }

    void OnDrawGizmos(){
        foreach(GameObject neighbor in neighbors){
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    void Update(){

    }
    public float GetHeuristic(){
        return heuristic;
    }
}
