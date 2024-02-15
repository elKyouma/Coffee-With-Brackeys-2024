using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIActivator : MonoBehaviour, IInteractable
{
    public GameObject POIObject;
    public bool Active { get; set; }

    private void OnEnable()
    {
        Active = true;
        Interactor.AddInteractable(transform);
    }

    private void OnDisable()
    {
        Active = false;
        Interactor.DeleteInteractable(transform);
    }

    public void Interact()
    {
        GameManager.Instance.EnterPuzzleMode(POIObject);
    }
    public void Selected()
    {
    }

    public void Unselected()
    {
    }
}
