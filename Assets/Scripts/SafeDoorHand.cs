using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoorHand : OutlineInteractable
{
    private bool isOpenable = false;
    private bool isOpen = false;
    private float angle = 90f;
    private float animationTime = 3f;
    public void Unlock()
    {
        isOpenable = true;
    }
    public override void Interact()
    {
        if (!isOpenable) return;
        if (isOpen) return;
        LeanTween.rotateLocal(gameObject, new Vector3(0, 0, angle), animationTime)
        .setOnComplete(() =>
        {
            transform.parent.GetComponentInChildren<SafeDoor>().Open();
            isOpen = true;
        });
    }
}
