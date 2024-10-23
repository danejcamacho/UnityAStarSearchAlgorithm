
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarNavigation : MonoBehaviour
{
    public GameObject navNodes;
    [SerializeField] private float carSpeed = 1;
    private GameObject currNode;
    private GameObject startNode;
    public GameObject debugNodeForTesting;
    private bool carIsMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeNode(debugNodeForTesting);
        }
    }

    public void ChangeNode(GameObject nodeToTravelTo){
        if(!carIsMoving){
            StartCoroutine(TravelToNode(nodeToTravelTo));
        }
    }

    private IEnumerator TravelToNode(GameObject nodeToTravelTo){

	    float time = 0;
        while (time < 1 ){
            time += Time.deltaTime * carSpeed;
            transform.position = Vector3.Lerp(currNode.transform.position, nodeToTravelTo.transform.position, time);
            yield return null;
        }

        currNode = nodeToTravelTo;
	
    }
}
