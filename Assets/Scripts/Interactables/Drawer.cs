using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : OutlineInteractable
{
    [SerializeField, Range(0.1f, 0.8f)] private float range;
    [SerializeField, Range(0.1f, 0.8f)] private float speed;
    private float startPosZ;
    private void Awake()
    {
        startPosZ = transform.localPosition.z;
    }

    public override void Interact()
    {
        if (transform.localPosition.z <= startPosZ + range * 0.5f)
            transform.LeanMoveLocalZ(startPosZ + range, speed).setEaseInOutQuad();
        else
            transform.LeanMoveLocalZ(startPosZ, speed).setEaseInOutQuad();
    }
}
