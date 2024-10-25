
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarNavigation : MonoBehaviour
{
    public GameObject navNodes;
    [SerializeField] private float carSpeed = 5;
    private GameObject currNode;
    private GameObject startNode;
    public GameObject debugNodeForTesting;
    private bool carIsMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        startNode = GameObject.FindWithTag("Start");
        currNode = startNode;
        transform.position = currNode.transform.position;
        carIsMoving = false;
        Debug.Log(startNode);
    }

    // Update is called once per frame
    void Update()
    {
        //gets a random index in neighbors and randomly travels to nodes (for debug purposes)
        int randIdx = Random.Range(0, currNode.GetComponent<Node>().neighbors.Count);
        ChangeNode(currNode.GetComponent<Node>().neighbors[randIdx]);
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
