using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class Inventory : MonoBehaviour
{
    private Player player;
    private IItem item;

    private void Start()
    {
        player = GetComponent<Player>();
    }
}
