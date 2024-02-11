using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour, IItem, IInteractable
{
    private bool interactable = true;
    private MeshRenderer meshRenderer;
    private void Awake() => meshRenderer = GetComponent<MeshRenderer>();
    
    public void Interact()
    {
        if (!interactable) return;

        interactable = false;

        Inventory.PickUpItem(this, gameObject);
    }

    public void Selected()
    {
        if (!interactable) return;
        
        meshRenderer.materials[1].SetFloat("_Thickness", 0.1f);
    }

    public void Unselected()
    {
        if (!interactable) return;
        
        meshRenderer.materials[1].SetFloat("_Thickness", -1f);
    }

    public void UseItem()
    {
        Debug.Log("Using " + name);
    }
}
