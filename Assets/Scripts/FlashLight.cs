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
    [SerializeField]
    private Light light;

    void Start()
    {
        ReplaceLight();
    }

    public void ReplaceLight()
    {
        light.transform.parent = inHand ? inHandLight : outsideHandLight;
        light.transform.localPosition = Vector3.zero;
        light.transform.localRotation = Quaternion.identity;
    }

    public override void UseItem()
    {
        lightObject.SetActive(!lightObject.activeSelf);
    }
}
