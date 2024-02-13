using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : OutlineInteractable, IDestructable
{
    [SerializeField] private SoundSO breakingSound;

    public void DestroyObject()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = transform.forward * (-2);
        SoundManager.Instance.PlaySound(breakingSound, transform.position);
    }

    public override void Interact()
    {
    }
}
