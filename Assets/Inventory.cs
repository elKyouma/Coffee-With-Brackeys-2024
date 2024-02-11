using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class Inventory : MonoBehaviour
{
    private static IItem item;
    private static GameObject itemObj;

    public static void PickUpItem(IItem newItem, GameObject newItemObj)
    {
        if (item == null)
            item = newItem;
        else
            Debug.Log("Brak miejsca w inventory");
        
        itemObj = newItemObj;

        itemObj.transform.parent = GameManager.Instance.HandObject;
        itemObj.transform.localPosition = Vector3.zero;
        itemObj.GetComponent<Collider>().enabled = false;
        Player.OnItemUseEvent += item.UseItem;
    }

    public static void DropItem()
    {
        itemObj.transform.parent = null;
        itemObj.GetComponent<Collider>().enabled = true;
        Player.OnItemUseEvent -= item.UseItem;
    }
}
