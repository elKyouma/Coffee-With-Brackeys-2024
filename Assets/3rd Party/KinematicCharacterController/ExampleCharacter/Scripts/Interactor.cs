using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float Threshold = 0.97f;
    
    private Camera playerCamera;
    private static List<Transform> interactables;
    private Transform previousSelection = null;
    private Transform currentSelection = null;
    private bool interact = false;

    private void OnEnable()
    {
        Player.OnInteraction += Interact;
    }

    private void OnDisable()
    {
        Player.OnInteraction -= Interact;
    }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        interactables = new List<Transform>(); 
    }

    Transform FindObjectClosestToCursor()
    {
        Transform result = null;

        float closest = 0f; 
        for (int i = 0; i < interactables.Count; i++)
        {
            Vector3 vector1 = playerCamera.transform.forward;
            Vector3 vector2 = interactables[i].position - playerCamera.transform.position;

            float lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);

            if (lookPercentage > closest && lookPercentage >= Threshold)
            {
                closest = lookPercentage;
                result = interactables[i];
            }

        }
        return result;
    }

    Transform FindObjectViaRayCast()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = playerCamera.ScreenPointToRay(screenCentre);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.GetComponent<IInteractable>() != null)
                return selection;
        }

        return null;
    }

    private void Update()
    {
        previousSelection = currentSelection;
        currentSelection = FindObjectViaRayCast();

        if (currentSelection == null)
            currentSelection = FindObjectViaRayCast();

        if (currentSelection != previousSelection)
        {
            previousSelection?.GetComponent<IInteractable>().Unselected();
            currentSelection?.GetComponent<IInteractable>().Selected();
        }

        if(interact)
            currentSelection?.GetComponent<IInteractable>().Interact();

    }

    public static void AddInteractable(Transform interactable) => interactables.Add(interactable);
    public static void DeleteInteractable(Transform interactable) => interactables.Remove(interactable);

    private void Interact()
    {
        previousSelection?.GetComponent<IInteractable>().Interact();

    }
}
