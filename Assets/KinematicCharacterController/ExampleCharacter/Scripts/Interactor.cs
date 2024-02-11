using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float Threshold = 0.97f;
    
    private List<Transform> interactables;
    private Transform _selection = null;

    private bool interact = false;

    private void OnEnable()
    {
        Player.OnInteraction += Interact;
    }

    private void OnDisable()
    {
        Player.OnInteraction -= Interact;
    }

    private void Start()
    {
        interactables = new List<Transform>(); 
    }

    private void Update()
    {
        Transform selection = null;
        float closest = 0f;

        for(int i = 0; i < interactables.Count; i++)
        {
            Vector3 vector1 = playerCamera.transform.forward;
            Vector3 vector2 = interactables[i].position - playerCamera.transform.position;

            float lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
            
            if(lookPercentage > closest && lookPercentage >= Threshold)
            {
                //Debug.Log(lookPercentage);
                closest = lookPercentage;
                

                selection = interactables[i];
            }

        }
        if(_selection != null && selection != _selection)
            _selection.GetComponent<IInteractable>().Unselected();
        
        if (selection != null && selection != _selection)
            selection.GetComponent<IInteractable>().Selected();

        if(selection != null && interact == true)
        {
            selection.GetComponent<IInteractable>().Interact();
        }

        _selection = selection;


    }

    public void AddInteractable(Transform interactable)
    {
        interactables.Add(interactable);
    }

    public void DeleteInteractable(Transform interactable)
    {
        interactables.Remove(interactable);
    }

    private void Interact()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = playerCamera.ScreenPointToRay(screenCentre);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.GetComponent<IInteractable>() != null)
            {
                if (_selection != selection)
                {
                    if (_selection != null)
                        _selection.GetComponent<IInteractable>().Unselected();
                    selection.GetComponent<IInteractable>().Selected();
                }
                _selection = selection;
            }
        }
    }
}
