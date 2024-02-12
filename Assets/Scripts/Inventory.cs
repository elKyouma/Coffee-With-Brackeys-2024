using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class Inventory : MonoBehaviour
{
    private static Item item;
    private static GameObject itemObj;
    public static void PickUpItem(Item newItem, GameObject newItemObj)
    {
        if (item == null)
            item = newItem;
        else
            Debug.Log("Brak miejsca w inventory");
        
        itemObj = newItemObj;

        itemObj.transform.parent = GameManager.Instance.HandObject;
        itemObj.transform.localPosition = Vector3.zero;
        itemObj.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        itemObj.GetComponent<Rigidbody>().isKinematic = true;
        itemObj.GetComponentInChildren<Collider>().enabled = false;
        Player.OnItemUseEvent += item.UseItem;
    }

    public static void DropItem()
    {
        itemObj.transform.parent = null;
        itemObj.GetComponent<Rigidbody>().isKinematic = false;
        itemObj.GetComponentInChildren<Collider>().enabled = true;
        Player.OnItemUseEvent -= item.UseItem;
    }
}
