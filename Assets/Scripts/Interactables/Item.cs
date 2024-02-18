using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(Rotatable)),SelectionBase]
public abstract class Item : MonoBehaviour, IInteractable
{
    private const string tag = "ItemCatcher";
    private bool interactable = true;
    private Renderer[] renderers;
    private Material outlineFillMaterial;
    public bool Active { get { return interactable && GameManager.Instance.state == GameManager.GameState.Normal; } }

    private void Awake()
    {
        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();
        outlineFillMaterial = renderers[0].material;
        // Instantiate outline materials

        // Disable Rotatable if exists
        if (GetComponentInChildren<Rotatable>())
            GetComponentInChildren<Rotatable>().rotateAllowed = false;
    }

    private void OnEnable()
    {
        TurnOffOutline();
        Interactor.AddInteractable(transform);

        foreach (var renderer in renderers)
        {

            // Append outline shaders
            var materials = renderer.sharedMaterials.ToList();

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
        outlineFillMaterial.SetFloat("_Frequency", 3f);
    }

    public void TurnOffOutline()
    {
        outlineFillMaterial.SetFloat("_Frequency", 0.0f);
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
