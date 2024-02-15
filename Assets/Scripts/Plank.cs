using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : OutlineInteractable, IDestructable
{
    private bool IsDestructable = true;
    [SerializeField] private SoundSO breakingSound;

    public void DestroyObject()
    {
        if (!IsDestructable) return;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = transform.forward * (-2);
        SoundManager.Instance.PlaySound(breakingSound, transform.position);
        IsDestructable = false;
        transform.parent.GetComponentInChildren<DoorWithPlanks>().IsOpenable();
    }

    public override void Interact()
    {
    }
}
