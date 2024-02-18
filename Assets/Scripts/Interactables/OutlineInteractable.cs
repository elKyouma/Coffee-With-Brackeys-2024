using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class OutlineInteractable : MonoBehaviour, IInteractable
{

    private Renderer[] renderers;
    private List<Material> materials;


    [SerializeField]
    GameManager.GameState state;
    public GameManager.GameState State { get { return state; } }

    private void SetUp()
    {
        // Instantiate outline materials
        renderers = GetComponentsInChildren<Renderer>();
        materials = new List<Material>();
        foreach (Renderer renderer in renderers)
            materials.AddRange(renderer.materials);

        Color color = Color.white;
        color.a = 0.4f;
        foreach (Material material in materials)
            material.SetColor("_OutlineColor", color);
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
        foreach (Material material in materials)
            material.SetFloat("_Frequency", 2f);
    }

    private void TurnOffOutline()
    {
        foreach (Material material in materials)
            material.SetFloat("_Frequency", 0f);
    }    
    public virtual void Selected() => TurnOnOutline();
    public void Unselected() => TurnOffOutline();
}
