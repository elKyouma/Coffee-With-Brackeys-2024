using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Item, IInteractable
{
    public override void UseItem()
    {
        Debug.Log("Using " + name);
    }
}
