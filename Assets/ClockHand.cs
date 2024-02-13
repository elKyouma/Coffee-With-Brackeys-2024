using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class ClockHand : OutlineInteractable
{
    public override void Interact()
    {
        Vector3 pointToLookAt = Camera.main.ScreenToWorldPoint(Player.MousePosition);
        transform.rotation = Quaternion.LookRotation(pointToLookAt - transform.position);
    }
}
