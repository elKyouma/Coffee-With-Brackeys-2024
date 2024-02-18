using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] private GameObject tooltipCanvas;
    [SerializeField] private Sprite[] tooltipTextures;

    private List<string> tooltipMessages = new List<string>();
    private List<TooltipData> activeTooltips = new List<TooltipData>();
    private Queue<TooltipData> tooltipPool = new Queue<TooltipData>();
    private bool needToUpdateTooltips = true;

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
            TooltipData tooltipData = new TooltipData(tooltipGO, tooltipGO.GetComponentInChildren<TextMeshProUGUI>(), tooltipGO.GetComponentInChildren<Image>());
            tooltipPool.Enqueue(tooltipData);
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
        needToUpdateTooltips = true;
    }

    private void CheckAndDisplayTooltipsBasedOnGameState()
    {
        // Example logic to add tooltips based on game state
        if (Interactor.Selection != null)
        {
            AddTooltip(" to interact", tooltipTextures[0]);
        }
        if (GameManager.Instance.HandObject.GetComponentInChildren<Item>() != null && GameManager.Instance.state == GameManager.GameState.Normal)
        {
            AddTooltip(" to use item", tooltipTextures[1]);
            AddTooltip(" to drop item", tooltipTextures[2]);
        }
        if (GameManager.Instance.HandObject.GetComponentInChildren<Rotatable>() != null && GameManager.Instance.state == GameManager.GameState.Normal)
        {
            AddTooltip(" to inspect", tooltipTextures[3]);
        }
        if (GameManager.Instance.state == GameManager.GameState.Inspect || GameManager.Instance.state == GameManager.GameState.Puzzle)
        {
            AddTooltip(" to exit", tooltipTextures[4]);
        }
        DisplayActiveTooltips();
    }

    void AddTooltip(string message, Sprite image)
    {
        if (!tooltipMessages.Contains(message))
        {
            tooltipMessages.Add(message);
            TooltipData tooltipData = GetTooltipFromPool();
            tooltipData.TextComponent.text = message;
            tooltipData.ImageComponent.sprite = image;
            activeTooltips.Add(tooltipData);
        }
    }

    TooltipData GetTooltipFromPool()
    {
        if (tooltipPool.Count > 0)
        {
            TooltipData pooledTooltip = tooltipPool.Dequeue();
            pooledTooltip.GameObject.SetActive(true);
            return pooledTooltip;
        }
        else
        {
            GameObject newTooltipGO = Instantiate(tooltipPrefab, tooltipCanvas.transform);
            return new TooltipData(newTooltipGO, newTooltipGO.GetComponentInChildren<TextMeshProUGUI>(), newTooltipGO.GetComponentInChildren<Image>());
        }
    }
    void DisplayActiveTooltips()
    {
        float screenHeight = Screen.height; // Get the screen height
        float screenWidth = Screen.width; // Get the screen width
        float tooltipHeight = 50; // Adjust based on actual height in your prefab
        float margin = 10; // Margin between tooltips
        float startingY = screenHeight * 0.1f; // Start 10% up from the bottom of the screen, adjust as needed

        for (int i = 0; i < activeTooltips.Count; i++)
        {
            // Calculate the Y position based on the screen height, tooltip height, and margin
            float yPos = startingY + (i * (tooltipHeight + margin));
            // Example: Set X position to 5% from the left edge of the screen
            float xPos = screenWidth * 0.05f;

            // Use cached RectTransform from TooltipData
            RectTransform tooltipRect = activeTooltips[i].GameObject.GetComponent<RectTransform>();
            tooltipRect.anchoredPosition = new Vector2(xPos, yPos);
        }
    }


    void ClearTooltips()
    {
        while (activeTooltips.Count > 0)
        {
            TooltipData tooltip = activeTooltips[0];
            activeTooltips.RemoveAt(0);
            ReturnTooltipToPool(tooltip);
        }
        tooltipMessages.Clear();
    }

    void ReturnTooltipToPool(TooltipData tooltipData)
    {
        tooltipData.GameObject.SetActive(false);
        tooltipPool.Enqueue(tooltipData);
    }
    class TooltipData
    {
        public GameObject GameObject;
        public TextMeshProUGUI TextComponent;
        public Image ImageComponent;

        public TooltipData(GameObject gameObject, TextMeshProUGUI textComponent, Image imageComponent)
        {
            GameObject = gameObject;
            TextComponent = textComponent;
            ImageComponent = imageComponent;
        }
    }
}
