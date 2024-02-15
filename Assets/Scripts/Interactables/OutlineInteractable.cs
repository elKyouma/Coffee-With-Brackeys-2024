using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class OutlineInteractable : MonoBehaviour, IInteractable
{

    private Renderer[] renderers;
    private Material outlineMaskMaterial;
    private Material outlineFillMaterial;

    [SerializeField]
    GameManager.GameState state;
    public GameManager.GameState State { get { return state; } }

    private void Awake()
    {

        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();

        // Instantiate outline materials
        outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
        outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

        outlineMaskMaterial.name = "OutlineMask (Instance)";
        outlineFillMaterial.name = "OutlineFill (Instance)";
    }

    private void OnEnable()
    {
        Interactor.AddInteractable(transform);

        foreach (var renderer in renderers)
        {

            // Append outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Add(outlineMaskMaterial);
            materials.Add(outlineFillMaterial);

            renderer.materials = materials.ToArray();
        }
    }
    private void OnDisable()
    {
        Interactor.DeleteInteractable(transform);

        foreach (var renderer in renderers)
        {

            // Remove outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Remove(outlineMaskMaterial);
            materials.Remove(outlineFillMaterial);

            renderer.materials = materials.ToArray();
        }
    }

    public abstract void Interact();

    private void TurnOnOutline()
    {
        // Apply properties according to mode
        outlineFillMaterial.SetColor("_OutlineColor", Color.yellow);

        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_OutlineWidth", 8f);
    }

    private void TurnOffOutline() => outlineFillMaterial.SetFloat("_OutlineWidth", 0.0f);
    public void Selected() => TurnOnOutline();
    public void Unselected() => TurnOffOutline();
}
