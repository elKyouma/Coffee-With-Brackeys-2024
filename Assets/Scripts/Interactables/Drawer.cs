using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : OutlineInteractable
{
    [SerializeField] private float range;
    private float startPosX;
    private void Awake()
    {
        startPosX = transform.localPosition.x;
    }

    public override void Interact()
    {
        if (transform.localPosition.x == startPosX)
            transform.LeanMoveLocalX(startPosX + range, 1f).setEaseInOutQuad();
        else
            transform.LeanMoveLocalX(startPosX, 1f).setEaseInOutQuad();
    }
}
