using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handout : Item, IInteractable
{
    private bool inUse = false;
    public Transform center;

    public override void UseItem()
    {
        
        if (!inUse)
        {
            LeanTween.moveLocal(gameObject, transform.InverseTransformPoint(center.position - transform.forward * 0.7f), 1);
            inUse = true;
        }
        else
        {
            LeanTween.moveLocal(gameObject, new Vector3(0f, 0f, 0f), 1);
            inUse = false;
        }
    }
}
