using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private const string defaultActionMap = "Movement";
    private const string puzzleModeActionMap = "PuzzleMode";
    private const string inspectorModeActionMap = "InspectorMode"; // Mode to inspect objects in inventory (rotatables)
    private GameObject ActivePOI;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform handObject;
    [SerializeField]
    private Transform playerCharacter;
    public Transform PlayerCharacter { get { return playerCharacter; } }
    public Transform HandObject { get { return handObject; } }

    [SerializeField]
    private Transform inspectedObjectTransform;
    [SerializeField]
    private Volume blurVolume;

    public void EnterPuzzleMode(GameObject go)
    {
        if (go == null) return;

        ActivePOI = go;
        ActivePOI.SetActive(true);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(puzzleModeActionMap);
    }

    public void ExitPuzzleMode()
    {
        ActivePOI.SetActive(false);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(defaultActionMap);
        ActivePOI = null;
    }
    public void EnterInspectorMode()
    {
        if (handObject.childCount == 0) return;
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(inspectorModeActionMap);
        var heldObject = handObject.GetComponentInChildren<Item>().gameObject;
        // heldObject.transform.position = inspectedObjectTransform.position;
        StartCoroutine(MoveToInspectedPosition(heldObject, inspectedObjectTransform, 0.5f));
        blurVolume.enabled = true;
        foreach (var children in inspectedObjectTransform.GetComponentsInChildren<Rotatable>())
        {
            children.enabled = true;
        }
    }
    public void ExitInspectorMode()
    {
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(defaultActionMap);
        var heldObject = handObject.GetComponentInChildren<Item>().gameObject;
        heldObject.transform.position = handObject.position;
        // reset rotation
        heldObject.transform.rotation = handObject.rotation;
        blurVolume.enabled = false;
        foreach (var children in inspectedObjectTransform.GetComponentsInChildren<Rotatable>())
        {
            children.enabled = false;
        }
    }

    IEnumerator MoveToInspectedPosition(GameObject objectToMove, Transform targetTransform, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < duration)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, targetTransform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = targetTransform.position;
    }

}