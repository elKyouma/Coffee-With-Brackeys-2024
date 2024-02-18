using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class OutlineInteractable : MonoBehaviour, IInteractable
{

    private Renderer[] renderers;
    private Material outlineFillMaterial;

    [SerializeField]
    GameManager.GameState state;
    public GameManager.GameState State { get { return state; } }

    private void SetUp()
    {
        // Instantiate outline materials
        renderers = GetComponentsInChildren<Renderer>();
        outlineFillMaterial = renderers[0].material;
        outlineFillMaterial.SetColor("_OutlineColor", Color.white);
    }

    private void OnEnable()
    {
        Interactor.AddInteractable(transform);
        if (renderers == null) SetUp();
    }
    private void OnDisable()
    {
        Interactor.DeleteInteractable(transform);
    }

    public abstract void Interact();

    public void TurnOnOutline()
    {
        // Apply properties according to mode
        outlineFillMaterial.SetFloat("_Frequency", 2f);
    }

    private void TurnOffOutline() => outlineFillMaterial.SetFloat("_Frequency", 0f);
    public virtual void Selected() => TurnOnOutline();
    public void Unselected() => TurnOffOutline();
}
