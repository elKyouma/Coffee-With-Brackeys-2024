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

        if(Input.GetMouseButtonUp(0)) rotateToCursor = false;

        //Vector2 direction = Player.MousePosition;
        //direction.x /= Screen.width;
        //direction.y /= Screen.height;
        //direction -= Vector2.one * 0.5f;
        //direction.x = -direction.x;

        //transform.rotation = Quaternion.LookRotation(direction, transform.up);

        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = Player.MousePosition - pos;
        dir.x = -dir.x;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir, transform.up);

    }
}
