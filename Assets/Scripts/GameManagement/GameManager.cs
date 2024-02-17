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
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;


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


    public enum GameState
    {
        Normal,
        Puzzle,
        Inspect
    }

    public GameState state;

    public void EnterPuzzleMode(GameObject go)
    {
        if (go == null || ActivePOI != null) return;
        Cursor.lockState = CursorLockMode.None;

        state = GameState.Puzzle;
        ActivePOI = go;
        ActivePOI.SetActive(true);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(puzzleModeActionMap);
    }
    public void ExitPuzzleMode()
    {
        float delay = 2f;
        StartCoroutine(DelayedExitPuzzleMode(delay));
    }

    IEnumerator DelayedExitPuzzleMode(float delay)
    {
        state = GameState.Normal;

        Cursor.lockState = CursorLockMode.Locked;
        ActivePOI.SetActive(false);
        yield return new WaitForSeconds(delay);
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(defaultActionMap);
        ActivePOI = null;
    }
    public void EnterInspectorMode()
    {
        state = GameState.Inspect;
        if (handObject.childCount == 0) return;

        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(inspectorModeActionMap);
        var heldObject = handObject.GetComponentInChildren<Item>().gameObject;
        StartCoroutine(MoveToInspectedPosition(heldObject, inspectedObjectTransform, 0.5f));
        blurVolume.enabled = true;
        heldObject.GetComponentInChildren<Rotatable>().rotateAllowed = true;
    }
    public void ExitInspectorMode()
    {
        state = GameState.Normal;

        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(defaultActionMap);
        var heldObject = handObject.GetComponentInChildren<Item>().gameObject;
        heldObject.transform.position = handObject.position;
        // reset rotation
        heldObject.transform.rotation = handObject.rotation;
        blurVolume.enabled = false;
        heldObject.GetComponentInChildren<Rotatable>().rotateAllowed = false;
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
    public void hideOrShow()
    {
        if (pauseMenu.activeInHierarchy == true)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 1;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}