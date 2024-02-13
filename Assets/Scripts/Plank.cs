using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : OutlineInteractable, IDestructable
{
    public void DestroyObject()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = transform.forward * (-2);
    }

    public override void Interact()
    {
    }
}
