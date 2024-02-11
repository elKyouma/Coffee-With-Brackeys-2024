using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CorrectOrderScript : MonoBehaviour
{
    public int squareCount = 3;
    public List<int> indexesOrder = new List<int>();
    public List<int> playerInputOrder = new List<int>();
    void Start()
    {
        InstantiateBlocks(squareCount);
    }
    void InstantiateBlocks(int squareCount)
    {
        // temporary
        for (int i = 0; i < squareCount; i++)
        {
            for (int j = 0; j < squareCount; j++)
            {
                GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
                square.transform.parent = this.transform;
                square.transform.position = new Vector3(i, j, 0);
                square.AddComponent<CorrectOrderBlockScript>();
                square.GetComponent<CorrectOrderBlockScript>().index = i * squareCount + j;
                if ((i + j) % 2 == 0)
                {
                    square.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    square.GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckOrder();
        }
    }
    void CheckOrder()
    {
        if (indexesOrder.Count() != playerInputOrder.Count)
        {
            Debug.Log("Wrong order");
            playerInputOrder.Clear();
            return;
        }
        for (int i = 0; i < indexesOrder.Count; i++)
        {
            if (indexesOrder[i] != playerInputOrder[i])
            {
                Debug.Log("Wrong order");
                playerInputOrder.Clear();
                return;
            }
        }
        Debug.Log("Correct order");
        playerInputOrder.Clear();
    }
}
