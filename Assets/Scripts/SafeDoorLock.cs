using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoorLock : OutlineInteractable
{
    private bool IsOpenable = true;
    public override void Interact()
    {
        if (!IsOpenable) return;
        Debug.Log("Door Open");
        transform.parent.GetComponentInChildren<SafeDoor>().Unlock();
        IsOpenable = false;
    }
}
