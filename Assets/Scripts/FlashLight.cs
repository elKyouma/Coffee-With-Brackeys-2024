using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Item, IInteractable
{
    [SerializeField]
    private GameObject lightObject;

    public override void UseItem()
    {
        lightObject.SetActive(!lightObject.activeSelf);
    }
}
