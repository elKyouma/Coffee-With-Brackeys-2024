using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class Inventory : MonoBehaviour
{
    public static Item ItemInHand { get; private set; }
    private static GameObject itemObj;
    public static void PickUpItem(Item newItem, GameObject newItemObj)
    {
        if (ItemInHand == null)
            ItemInHand = newItem;
        else
        {
            DropItem();
            ItemInHand = newItem;
        }

        itemObj = newItemObj;

        itemObj.transform.parent = GameManager.Instance.HandObject;
        itemObj.transform.localPosition = Vector3.zero;
        itemObj.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        itemObj.GetComponent<Rigidbody>().isKinematic = true;
        itemObj.GetComponentInChildren<Collider>().enabled = false;
        itemObj.layer = LayerMask.NameToLayer("Hand Object");
        foreach (Transform x in itemObj.GetComponentsInChildren<Transform>())
            x.gameObject.layer = LayerMask.NameToLayer("Hand Object");
        Player.OnItemUseEvent += ItemInHand.UseItem;

        if (ItemInHand is FlashLight)
        {
            FlashLight flashLight = (FlashLight)ItemInHand;
            flashLight.inHand = true;
            flashLight.ReplaceLight();
        }
    }

    public static void DropItem()
    {
        itemObj.transform.parent = null;
        itemObj.GetComponent<Rigidbody>().isKinematic = false;
        itemObj.GetComponentInChildren<Collider>().enabled = true;
        ItemInHand.Drop();
        itemObj.layer = LayerMask.NameToLayer("Default");
        foreach (Transform x in itemObj.GetComponentsInChildren<Transform>())
            x.gameObject.layer = LayerMask.NameToLayer("Default");
        Player.OnItemUseEvent -= ItemInHand.UseItem;
    }
}
