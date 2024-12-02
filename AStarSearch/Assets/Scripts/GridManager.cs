using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject goalNodePrefab;
    public GameObject startNodePrefab;
    private GameObject currNode;
    public int width = 10;
    public int height = 10;
    public float cellSize = 5f;

    private int[,] gridArray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        gridArray = new int[width, height];

        //instantiate our objects in a grid
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.Log(x + ", " + y);
                
                //NOTE: Change this to make the indexes more accurate to where the car should actually start and end up

                if (x == 0 && y == 0){
                    currNode = Instantiate(startNodePrefab, new Vector3(x, 0.5f, y) * cellSize, Quaternion.identity, transform);
                } else if ( x == width - 1 && y == height - 1){
                    currNode = Instantiate(goalNodePrefab, new Vector3(x, 0.5f, y) * cellSize, Quaternion.identity, transform);
                } else {
                    currNode = Instantiate(nodePrefab, new Vector3(x, 0.5f, y) * cellSize, Quaternion.identity, transform);
                }

                currNode.name = "Node_" + x + "_" + y;
                
                
            }
        }       

        //set each objects neighbors (i wish i didnt have to loop through all of them again)
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                currNode = GameObject.Find("Node_" + x + "_" + y);
                //up
                if(y < gridArray.GetLength(1) - 1){
                    var nodeUp = GameObject.Find("Node_" + x + "_" + (y + 1));
                    if (nodeUp != null)
                    { 
                        currNode.GetComponent<Node>().neighbors.Add(nodeUp);
                    }
                }
                //down
                if(y > 0){
                    var nodeDown = GameObject.Find("Node_" + x + "_" + (y - 1));
                    if (nodeDown != null)
                    { 
                        currNode.GetComponent<Node>().neighbors.Add(nodeDown);
                    }                
                }
                //right
                if(x < gridArray.GetLength(0) - 1){
                    var nodeRight = GameObject.Find("Node_" + (x + 1) + "_" + y);
                    if (nodeRight!= null)
                    { 
                        currNode.GetComponent<Node>().neighbors.Add(nodeRight);
                    }                
                }
                //left
                if(x > 0){
                    var nodeLeft = GameObject.Find("Node_" + (x - 1) + "_" + y);
                    if (nodeLeft!= null)
                    { 
                        currNode.GetComponent<Node>().neighbors.Add(nodeLeft);
                    }               
                }

                //set goal
                currNode.GetComponent<Node>().goalNode = GameObject.FindWithTag("Goal");
            }
        }        
    }
}
