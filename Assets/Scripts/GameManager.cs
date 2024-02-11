using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private const string defaultActionMap = "Movement";
    private const string puzzleModeActionMap = "PuzzleMode";
    private GameObject ActivePOI;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform handObject;
    [SerializeField]
    private Transform playerCharacter;
    public Transform PlayerCharacter { get { return playerCharacter; }}
    public Transform HandObject { get { return handObject; } }

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
        ActivePOI=null;
    }
}
