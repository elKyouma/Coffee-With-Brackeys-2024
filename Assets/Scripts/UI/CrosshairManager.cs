using UnityEngine;
using UnityEngine.UI; // Make sure to include this for working with UI elements

public class CrosshairManager : MonoBehaviour
{
    [SerializeField]
    private Image crosshairImage; // Assign this in the Inspector with your crosshair UI element
    [SerializeField] private Color onItemColor;
    [SerializeField] private Color onInteractableColor;
    [SerializeField] private Color defaultColor;


    private void Update()
    {
        var state = GameManager.Instance.state;
        var selection = Interactor.Selection;
        if (!selection)
        {
            crosshairImage.color = defaultColor;
        }
        else
        {
            if (selection.GetComponent<Item>() != null)
            {
                crosshairImage.color = onItemColor;
            }
            else
            {
                crosshairImage.color = onInteractableColor;
            }
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
