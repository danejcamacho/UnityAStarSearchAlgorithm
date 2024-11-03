
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarNavigation : MonoBehaviour
{
    public GameObject navNodes;
    [SerializeField] private float carSpeed = 5;
    private GameObject currNode;
    private GameObject startNode;
    private bool carIsMoving = false;
    private bool carIsSearching = false;

    [Header("A Star Algorithm")]
    //List of nodes to be visited
    List<GameObject> closedList = new List<GameObject>();

    //Dict of nodes that have been visited + the cost to get there + heuristic
    Dictionary<GameObject, float> openList = new Dictionary<GameObject, float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        startNode = GameObject.FindWithTag("Start");
        currNode = startNode;
        transform.position = currNode.transform.position;
        carIsMoving = false;
        Debug.Log(startNode);


        
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && !carIsSearching){
            AStarSearch();
        }
    }

    private void AStarSearch(){
        carIsSearching = true;
        currNode = startNode;
        //add the start node to the open list
        openList.Add(currNode, 0 + currNode.GetComponent<Node>().GetHeuristic());
        while(openList.Count > 0){
            //add all the neighbors of the current node to the open list
            //    if the neighbor is not in the closed list
            //    if the neighbor is in the neighbor list, update the cost if it is less
            


            //get lowest cost node in the open list,
            //   add it to the closed list,
            //   remove it from the open list
            //   set the current node to the lowest cost node
            //   use ChangeNode() to move the car to the current node (maybe use await?)
            
            //if the current node is the goal node, break

            //
        }
        carIsSearching = false;
    }


    /// <summary>
    /// This function will move the car to the specified node
    /// </summary>
    /// <param name="nodeToTravelTo"></param>
    public void ChangeNode(GameObject nodeToTravelTo){
        if(!carIsMoving){
            StartCoroutine(TravelToNode(nodeToTravelTo));
        }
    }

    private IEnumerator TravelToNode(GameObject nodeToTravelTo){
        carIsMoving = true;
	    float time = 0;
        while (time < 1 ){
            time += Time.deltaTime * carSpeed;
            transform.position = Vector3.Lerp(currNode.transform.position, nodeToTravelTo.transform.position, time);
            yield return null;
        }

        currNode = nodeToTravelTo;
        carIsMoving = false;
	
    }
}
