using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;

    private int[,] gridArray;

    public Grid(int width, int height, float cellSize, GameObject nodePrefab)
    {
        this.width = width;
        this.height = height;
        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.Log(x + ", " + y);
                //GameObject gameObject = new GameObject("Node_" + x + "_" + y);
                //gameObject.transform.localPosition = new Vector3(x, y, 0) * cellSize;

                Instantiate(nodePrefab, new Vector3(x, 0, y) * cellSize, Quaternion.identity, transform);
                
                
            }
        }       
    }





}
