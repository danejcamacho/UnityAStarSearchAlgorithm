
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CarNavigation : MonoBehaviour
{
    public GameObject navNodes;
    [SerializeField] private float carSpeed = 5;
    private GameObject currNode; // this is for the A* algorithm

    private GameObject currentNode; // this is for lerping
    private GameObject startNode;
    private bool carIsMoving = false;
    private bool carIsSearching = false;

    [Header("A Star Algorithm")]
    //List of nodes to be visited
    List<GameObject> closedList = new List<GameObject>();

    //Dict of nodes that have been visited + the cost to get there + heuristic
    Dictionary<GameObject, float> openList = new Dictionary<GameObject, float>();


    int navIdx = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        startNode = GameObject.FindWithTag("Start");
        currNode = startNode;
        currentNode = startNode;
        transform.position = startNode.transform.position;
        carIsMoving = false;
        AStarSearch();

        for (int i = 0; i < closedList.Count; i++){
            Debug.Log("Closed List: "+ i + " " + closedList[i]);
        }

        
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Z)){
            Debug.Log("navIdx: " + navIdx);
            navIdx++;
            ChangeNode(closedList[navIdx]);
        }




    }

    private void AStarSearch(){
        carIsSearching = true;
        currNode = startNode;
        //add the start node to the open list
        openList.Add(currNode, 0);
        while(openList.Count > 0){
            // Debug.Log("Current Node: " + currNode);
            // Debug.Log("OPEN LIST: ");
            // foreach(KeyValuePair<GameObject, float> node in openList){
            //     Debug.Log(node.Key + " " + node.Value);
            // }
            // Debug.Log("CLOSED LIST: ");
            // foreach(GameObject node in closedList){
            //     Debug.Log(node);
            // }
            //Debug.Log("Ran");
            closedList.Add(currNode);
            openList.Remove(currNode);

            //add all the neighbors of the current node to the open list
            //    if the neighbor is not in the closed list
            //    if the neighbor is in the neighbor list, update the cost if it is less
            foreach(GameObject neighbor in currNode.GetComponent<Node>().neighbors) {
                //Debug.Log("Neighbor: " + neighbor);
                if(!closedList.Contains(neighbor)){
                    if(openList.ContainsKey(neighbor)){
                        if(openList[neighbor] > openList[currNode] + currNode.GetComponent<Node>().GetCost()){
                            openList[neighbor] = openList[currNode] + currNode.GetComponent<Node>().GetCost();
                        }
                    } else {
                        openList.Add(neighbor, neighbor.GetComponent<Node>().GetCost() + currNode.GetComponent<Node>().GetCost() + neighbor.GetComponent<Node>().GetHeuristic());
                    }
                }
            }
            Debug.Log("Got past adding neighbors");
            
            //get lowest cost node in the open list,
            //   add it to the closed list,
            //   remove it from the open list
            //   set the current node to the lowest cost node
            float lowestCost = float.MaxValue;
            GameObject lowestCostNode = null;
            foreach(KeyValuePair<GameObject, float> node in openList){
                if(node.Value < lowestCost){
                    lowestCost = node.Value;
                    lowestCostNode = node.Key;
                }
            }

            //Debug.Log("Lowest Cost Node: " + lowestCostNode);

            if (lowestCostNode != null) {
                closedList.Add(lowestCostNode);
                openList.Remove(lowestCostNode);
                currNode = lowestCostNode;


            }


            // Check if the current node is the goal node
            if (currNode.CompareTag("Goal")){ {
                Debug.Log("Goal reached!");
                break;
            }
        }
        carIsSearching = false;
        }



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
            transform.position = Vector3.Lerp(currentNode.transform.position, nodeToTravelTo.transform.position, time);
            yield return null;
        }

        currentNode = nodeToTravelTo;
        carIsMoving = false;
	
    }
}
