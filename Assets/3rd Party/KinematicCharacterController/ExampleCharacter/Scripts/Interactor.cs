using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float Threshold = 0.97f;
    [SerializeField] private float InteractionDistance = 3f;

    private Camera playerCamera;
    private static List<Transform> interactables;
    private Transform previousSelection = null;
    public static Transform Selection { get; private set; }

    [SerializeField] private LayerMask interactMask;

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

    private void Start()
    {
        foreach (var x in interactables)
            x.GetComponent<IInteractable>().Unselected();
    }

    Transform FindObjectClosestToCursor()
    {
        Transform result = null;

        float closest = 0f;
        for (int i = 0; i < interactables.Count; i++)
        {
            if (interactables[i].GetComponent<OutlineInteractable>()?.State != GameManager.Instance.state) continue;
            if (Vector3.Distance(interactables[i].position, playerCamera.transform.position) > InteractionDistance)
                continue;
            Vector3 vector1 = playerCamera.transform.forward;
            Vector3 vector2 = interactables[i].position - playerCamera.transform.position;

            float lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
            if (lookPercentage > closest && lookPercentage >= Threshold)
            {
                closest = lookPercentage;
                result = interactables[i];
            }
        }
        TooltipManager.Instance.RequestTooltipUpdate();

        return result;
    }

    Transform FindObjectViaRayCast()
    {

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray;
        if (GameManager.Instance.state != GameManager.GameState.Normal)
            ray = playerCamera.ScreenPointToRay(Player.MousePosition);
        else
            ray = playerCamera.ScreenPointToRay(screenCentre);

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 4f, interactMask.value) && Vector3.Distance(playerCamera.transform.position, hit.point) <= InteractionDistance)
        {
            var selection = hit.transform;
            if (selection.GetComponent<IInteractable>() != null && (selection.GetComponent<OutlineInteractable>() == null || selection.GetComponent<OutlineInteractable>()?.State == GameManager.Instance.state))
                return selection;
        }


        return null;
    }

    private void Update()
    {
        previousSelection = Selection;
        Selection = FindObjectViaRayCast();

        if (Selection == null)
            Selection = FindObjectClosestToCursor();

        if (Selection != previousSelection)
        {
            previousSelection?.GetComponent<IInteractable>()?.Unselected();
            Selection?.GetComponent<IInteractable>().Selected();
            TooltipManager.Instance.RequestTooltipUpdate();
        }

    }

    public static void AddInteractable(Transform interactable) => interactables.Add(interactable);
    public static void DeleteInteractable(Transform interactable) => interactables.Remove(interactable);

    private void Interact()
    {
        Selection?.GetComponent<IInteractable>().Interact();
    }
}
