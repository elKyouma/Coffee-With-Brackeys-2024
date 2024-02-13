using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : Item
{
    [SerializeField]
    private Transform toRotate;

    public override void UseItem()
    {
        LeanTween.rotateLocal(toRotate.gameObject, new Vector3(-45f, 90f, 90f), 0.1f).setEaseInOutBack().setOnComplete(() => LeanTween.rotateLocal(toRotate.gameObject, new Vector3(0f, 90f, 90f), 0.4f).setEaseInOutBack());
        Interactor.Selection?.GetComponent<IDestructable>()?.DestroyObject();
    }
}
