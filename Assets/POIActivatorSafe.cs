using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class POIActivatorSafe : MonoBehaviour, IInteractable
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
        if (!transform.parent.GetComponentInChildren<SafeDoorHand>().IsOpen())
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GameManager.Instance.EnterPuzzleMode(POIObject);
        }
    }
    public void Selected()
    {
    }

    public void Unselected()
    {
    }
}
