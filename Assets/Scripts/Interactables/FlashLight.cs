using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Item, IInteractable
{
    [SerializeField]
    private GameObject lightObject;
    public bool inHand = false;
    [SerializeField]
    private Transform inHandLight; // Where the light will be when in hand
    [SerializeField]
    private Transform outsideHandLight; // Where the light will be when outside of hand

    void Start()
    {
        ReplaceLight();
    }

    public void ReplaceLight()
    {
        lightObject.transform.parent = inHand ? inHandLight : outsideHandLight;
        lightObject.transform.localPosition = Vector3.zero;
        lightObject.transform.localRotation = Quaternion.identity;
    }

    public override void UseItem()
    {
        lightObject.SetActive(!lightObject.activeSelf);
    }
}
