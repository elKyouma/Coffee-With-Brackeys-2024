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
    private List<Material> materials;
    public bool Active { get { return interactable && GameManager.Instance.state == GameManager.GameState.Normal; } }

    private void Awake()
    {
        // Cache renderers
        renderers = GetComponentsInChildren<Renderer>();
        materials = new List<Material>();
        foreach (Renderer renderer in renderers)
            materials.AddRange(renderer.materials);

        foreach (Material material in materials)
            material.SetColor("_OutlineColor", Color.yellow);

        // Instantiate outline materials

        // Disable Rotatable if exists
        if (GetComponentInChildren<Rotatable>())
            GetComponentInChildren<Rotatable>().rotateAllowed = false;
    }

    private void OnEnable()
    {
        TurnOffOutline();
        Interactor.AddInteractable(transform);
    }

    private void OnDisable()
    {
        Interactor.DeleteInteractable(transform);
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
        foreach(Material material in materials)
            material.SetFloat("_Frequency", 3f);
    }

    public void TurnOffOutline()
    {
        foreach (Material material in materials)
            material.SetFloat("_Frequency", 0.0f);
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
