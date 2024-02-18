using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    [SerializeField] private float timeDelay;
    void Start()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(timeDelay);
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Corridor");
    }

}
