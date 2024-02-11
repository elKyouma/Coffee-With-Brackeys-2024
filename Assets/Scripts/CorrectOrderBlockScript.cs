using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorrectOrderBlockScript : MonoBehaviour
{
    public int index;
    private CorrectOrderScript correctOrderScript;
    void Start()
    {
        correctOrderScript = transform.parent.GetComponent<CorrectOrderScript>();
    }

    void OnMouseDown()
    {
        correctOrderScript.playerInputOrder.Add(index);
    }
}
