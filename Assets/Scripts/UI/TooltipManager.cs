using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    [SerializeField] private GameObject tooltipPrefab; // Assign in Inspector
    private List<string> tooltipMessages = new List<string>(); // Stores unique tooltip messages
    private List<GameObject> activeTooltips = new List<GameObject>(); // Tracks active tooltip GameObjects
    private Queue<GameObject> tooltipPool = new Queue<GameObject>(); // Object pool for tooltips
    [SerializeField] private GameObject tooltipCanvas;
    private bool needToUpdateTooltips = true; // Flag to control tooltip updates

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep instance alive across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        PrepopulatePool(5); // Adjust number based on expected maximum tooltips
    }

    private void PrepopulatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject tooltipGO = Instantiate(tooltipPrefab, tooltipCanvas.transform);
            tooltipGO.SetActive(false);
            tooltipPool.Enqueue(tooltipGO);
        }
    }

    void Update()
    {
        if (needToUpdateTooltips)
        {
            ClearTooltips();
            CheckAndDisplayTooltipsBasedOnGameState();
            needToUpdateTooltips = false;
        }
    }
    public void RequestTooltipUpdate()
    {
        needToUpdateTooltips = true; // Public method to allow external scripts to request a tooltip update
    }

    private void CheckAndDisplayTooltipsBasedOnGameState()
    {
        // Example logic to add tooltips based on game state
        if (Interactor.Selection != null && Interactor.Selection.GetComponent<IInteractable>() != null)
        {
            AddTooltip("E to interact");
        }
        if (GameManager.Instance.HandObject.GetComponentInChildren<Item>() != null && GameManager.Instance.state == GameManager.GameState.Normal)
        {
            AddTooltip("^ to use item");
            AddTooltip("Q to drop item");
        }
        if (GameManager.Instance.HandObject.GetComponentInChildren<Rotatable>() != null && GameManager.Instance.state == GameManager.GameState.Normal)
        {
            AddTooltip("R to inspect");
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
            GameObject tooltipGO = GetTooltipFromPool();
            TextMeshProUGUI tooltipText = tooltipGO.GetComponentInChildren<TextMeshProUGUI>();
            tooltipText.text = message;
            activeTooltips.Add(tooltipGO);
        }
    }

    GameObject GetTooltipFromPool()
    {
        if (tooltipPool.Count > 0)
        {
            GameObject pooledTooltip = tooltipPool.Dequeue();
            pooledTooltip.SetActive(true);
            return pooledTooltip;
        }
        else
        {
            return Instantiate(tooltipPrefab, tooltipCanvas.transform);
        }
    }

    void DisplayActiveTooltips()
    {
        float screenHeight = Screen.height; // Get the screen height
        float screenWidth = Screen.width; // Get the screen width

        // Assume each tooltip's height is dynamically calculated or fixed
        float tooltipHeight = 50; // Adjust based on actual height in your prefab
        float margin = 10; // Margin between tooltips
        float startingY = screenHeight * 0.1f; // Start 10% up from the bottom of the screen, adjust as needed

        for (int i = 0; i < activeTooltips.Count; i++)
        {
            // Dynamically calculate the Y position based on the screen height, tooltip height, and margin
            float yPos = startingY + (i * (tooltipHeight + margin));

            // Dynamically set the X position or keep it constant if you want tooltips aligned in a specific way
            float xPos = screenWidth * 0.05f; // Example: 5% from the left edge of the screen

            // Update the RectTransform anchoredPosition to position tooltips dynamically
            RectTransform tooltipRect = activeTooltips[i].GetComponent<RectTransform>();
            tooltipRect.anchoredPosition = new Vector2(xPos, yPos);
        }
    }


    void ClearTooltips()
    {
        while (activeTooltips.Count > 0)
        {
            GameObject tooltip = activeTooltips[0];
            activeTooltips.RemoveAt(0);
            ReturnTooltipToPool(tooltip);
        }
        tooltipMessages.Clear();
    }

    void ReturnTooltipToPool(GameObject tooltip)
    {
        tooltip.SetActive(false);
        tooltipPool.Enqueue(tooltip);
    }
}
