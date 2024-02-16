using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;

public class ClockHand : OutlineInteractable
{
    bool rotateToCursor = false;
    public override void Interact()
    {
        rotateToCursor = true;
    }

    private void Update()
    {
        if (!rotateToCursor) return;

        if (Input.GetMouseButtonUp(0)) rotateToCursor = false;

        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = Player.MousePosition - pos;
        dir.x = -dir.x;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(new Vector3(0f, dir.y, dir.x), transform.up);

    }
}
