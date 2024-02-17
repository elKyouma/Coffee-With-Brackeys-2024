using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISafe : MonoBehaviour, IInteractable
{
    public GameObject POIObject;
    private void OnEnable()
    {
        Interactor.AddInteractable(transform);
    }

    private void OnDisable()
    {
        Interactor.DeleteInteractable(transform);
    }

    public void Interact()
    {
        transform.parent.GetComponentInChildren<SafeDoorLock>().OpeningFailed(true);
        GameManager.Instance.EnterPuzzleMode(POIObject);
    }
    public void Selected()
    {
    }

    public void Unselected()
    {
    }
}