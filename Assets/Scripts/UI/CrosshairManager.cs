using UnityEngine;
using UnityEngine.UI; // Make sure to include this for working with UI elements

public class CrosshairManager : MonoBehaviour
{
    [SerializeField]
    private Image crosshairImage; // Assign this in the Inspector with your crosshair UI element
    private Color onItemColor;
    private Color onInteractableColor;

    private void Update()
    {
        var state = GameManager.Instance.state;
        if (GameManager.Instance.HandObject.GetComponentInChildren<IInteractable>() != null)
        {
            if (GameManager.Instance.HandObject.GetComponentInChildren<Item>() != null)
            {
                crosshairImage.color = onItemColor;
            }
            else
            {
                crosshairImage.color = onInteractableColor;
            }
        }
        else
        {
            crosshairImage.color = Color.white;
        }
        switch (state)
        {
            case GameManager.GameState.Normal:
                crosshairImage.enabled = true;
                break;
            case GameManager.GameState.Puzzle:
                crosshairImage.enabled = false;
                break;
            case GameManager.GameState.Inspect:
                crosshairImage.enabled = false;
                break;
            case GameManager.GameState.Pause:
                crosshairImage.enabled = false;
                break;
            case GameManager.GameState.Options:
                crosshairImage.enabled = false;
                break;
            default:
                crosshairImage.enabled = false;
                break;
        }
    }
}
