using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handout : Item, IInteractable
{
    private bool inUse = false;

    public override void UseItem()
    {
        
        if (!inUse)
        {
            LeanTween.moveLocal(gameObject, new Vector3(-0.75f, 0.4f, -0.5f), 1);
            inUse = true;
        }
        else
        {
            LeanTween.moveLocal(gameObject, new Vector3(0f, 0f, 0f), 1);
            inUse = false;
        }
    }
}
