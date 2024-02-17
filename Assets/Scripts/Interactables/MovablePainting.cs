using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePainting : MonoBehaviour
{
    [SerializeField] private GameObject paintingModel;
    private bool hasMoved = false;
    public void MovePainting()
    {
        if(!hasMoved)
        {
            LeanTween.moveLocal(paintingModel, Vector3.zero, 3f);
            hasMoved = true;
        }
    }
}
