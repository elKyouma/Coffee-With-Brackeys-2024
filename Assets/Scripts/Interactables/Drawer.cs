using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : OutlineInteractable
{
    [SerializeField] private float range;
    private float startPosZ;
    private void Awake()
    {
        startPosZ = transform.localPosition.z;
    }

    public override void Interact()
    {
        if (transform.localPosition.z <= startPosZ + range * 0.5f)
            transform.LeanMoveLocalZ(startPosZ + range, 1f).setEaseInOutQuad();
        else
            transform.LeanMoveLocalZ(startPosZ, 1f).setEaseInOutQuad();
    }
}
