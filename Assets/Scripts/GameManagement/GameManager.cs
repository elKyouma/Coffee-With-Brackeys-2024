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
    private const string pauseActionMap = "Pause";
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
    [Header("UI")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;

    public enum GameState
    {
        Normal,
        Puzzle,
        Inspect,
        Pause,
        Options
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

    public void Back(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        switch (state)
        {
            case GameState.Options:
                HideOptions();
                break;
            case GameState.Pause:
                HidePause();
                break;
            default:
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HideOptions()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        state = GameState.Pause;
    }

    public void ShowPause()
    {
        if (state == GameState.Options) return;
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(pauseActionMap);
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        state = GameState.Pause;
        Time.timeScale = 0;
    }

    public void HidePause()
    {
        if (state != GameState.Pause) return;
        player.GetComponent<PlayerInput>().SwitchCurrentActionMap(defaultActionMap);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        state = GameState.Normal;
        Time.timeScale = 1;
    }
    public void ShowOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        state = GameState.Options;
    }
}