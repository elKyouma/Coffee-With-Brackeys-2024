using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(Rotatable)), SelectionBase]
public abstract class Item : MonoBehaviour, IInteractable
{
    private const string tag = "ItemCatcher";
    private bool interactable = true;
    private Renderer[] renderers;
    private Material outlineMaskMaterial;
    private Material outlineFillMaterial;
    public bool Active { get { return interactable && GameManager.Instance.state == GameManager.GameState.Normal; } }

    private void Awake()
    {
        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();

        // Instantiate outline materials
        outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
        outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

        outlineMaskMaterial.name = "OutlineMask (Instance)";
        outlineFillMaterial.name = "OutlineFill (Instance)";

        // Disable Rotatable if exists
        if (GetComponentInChildren<Rotatable>())
            GetComponentInChildren<Rotatable>().rotateAllowed = false;
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

    public void Interact()
    {
        if (!Active) return;

        interactable = false;
        Inventory.PickUpItem(this, gameObject);
    }

    public void Drop()
    {
        interactable = true;
    }

    public void TurnOnOutline()
    {
        // Apply properties according to mode
        outlineFillMaterial.SetColor("_OutlineColor", Color.yellow);

        outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        outlineFillMaterial.SetFloat("_OutlineWidth", 7f);
        TooltipManager.Instance.RequestTooltipUpdate();
    }

    public void TurnOffOutline()
    {
        outlineFillMaterial.SetFloat("_OutlineWidth", 0.0f);
        TooltipManager.Instance.RequestTooltipUpdate();
    }

    public void Selected()
    {
        if (!Active) return;

        TurnOnOutline();
    }

    public void Unselected()
    {
        if (!Active) return;

        TurnOffOutline();
    }

    public abstract void UseItem();
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tag)) return;

        transform.position = GameManager.Instance.PlayerCharacter.position;
        transform.position += Vector3.up;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
