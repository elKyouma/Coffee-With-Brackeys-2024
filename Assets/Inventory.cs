using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class Inventory : MonoBehaviour
{
    private Player player;
    private Item item;

    private void Start()
    {
        player = GetComponent<Player>();
    }
}
