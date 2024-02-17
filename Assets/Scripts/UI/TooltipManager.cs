using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    [SerializeField] private GameObject tooltipPrefab; // Assign in Inspector
    [SerializeField] private Transform tooltipContainer; // UI Container for Tooltips

    private List<string> tooltipMessages = new List<string>(); // Stores unique tooltip messages
    private List<GameObject> activeTooltips = new List<GameObject>(); // Tracks active tooltip GameObjects

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        // Object Pooling
        for (int i = 0; i < 4; i++)
        {

        }
    }

    void Update()
    {
        ClearTooltips(); // Clears existing tooltips before updating

        // Example logic to add tooltips based on game state
        if (Interactor.Selection != null && Interactor.Selection.GetComponent<IInteractable>() != null)
        {
            AddTooltip("E to interact");
        }
        if (GameManager.Instance.HandObject.GetComponentInChildren<Item>() != null && GameManager.Instance.state == GameManager.GameState.Normal)
        {
            AddTooltip("^ to use item");
        }
        if (GameManager.Instance.HandObject.GetComponentInChildren<Rotatable>() != null && GameManager.Instance.state == GameManager.GameState.Normal)
        {
            AddTooltip("I to inspect");
        }
        if (GameManager.Instance.state == GameManager.GameState.Inspect || GameManager.Instance.state == GameManager.GameState.Puzzle)
        {
            AddTooltip("Esc to exit");
        }

        DisplayActiveTooltips();
    }

    void AddTooltip(string message)
    {
        if (!tooltipMessages.Contains(message)) // Check to prevent duplicate messages
        {
            tooltipMessages.Add(message);
            GameObject tooltipGO = Instantiate(tooltipPrefab, tooltipContainer);
            TextMeshProUGUI tooltipText = tooltipGO.GetComponentInChildren<TextMeshProUGUI>();
            tooltipText.text = message;
            activeTooltips.Add(tooltipGO);
        }
    }

    void DisplayActiveTooltips()
    {
        int tooltipHeight = 50; // Assuming each tooltip is 50 units high
        int margin = 10; // Margin between tooltips
        int startingY = 10; // Starting Y position from the bottom of the screen

        for (int i = 0; i < activeTooltips.Count; i++)
        {
            // Calculate the Y position for each tooltip
            // As 'i' increases, tooltips move up because we're adding to the Y position
            int yPos = startingY + (i * (tooltipHeight + margin));

            // Set the anchoredPosition for each tooltip
            // Use a negative X position if you want some padding from the left edge of the screen
            activeTooltips[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(10, yPos);
        }
    }


    void ClearTooltips()
    {
        foreach (GameObject tooltip in activeTooltips)
        {
            Destroy(tooltip);
        }
        activeTooltips.Clear();
        tooltipMessages.Clear();
    }
}
