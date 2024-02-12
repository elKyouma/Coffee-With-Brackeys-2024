using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class Item : MonoBehaviour, IInteractable
{
    private bool interactable = true;
    private Renderer[] renderers;
    private Material outlineMaskMaterial;
    private Material outlineFillMaterial;
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
        Interactor.AddInteractable(transform);

        foreach (var renderer in renderers)
        {

            // Remove outline shaders
            var materials = renderer.sharedMaterials.ToList();

            materials.Remove(outlineMaskMaterial);
            materials.Remove(outlineFillMaterial);

            renderer.materials = materials.ToArray();
        }
    }

    public void Interact()
    {
        if (!interactable) return;

        interactable = false;
        Inventory.PickUpItem(this, gameObject);
    }

    private void TurnOnOutline()
    {
        // Apply properties according to mode
        outlineFillMaterial.SetColor("_OutlineColor", Color.white);

        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_OutlineWidth", 3f);
    }

    private void TurnOffOutline()
    {
        outlineFillMaterial.SetFloat("_OutlineWidth", 0.0f);
    }

    public void Selected()
    {
        if (!interactable) return;

        TurnOnOutline();

    }

    public void Unselected()
    {
        if (!interactable) return;

        TurnOffOutline();
    }

    public abstract void UseItem();
}
