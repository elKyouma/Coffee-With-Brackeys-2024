using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;
using UnityEngine.UIElements;
using System.Security.Permissions;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class SafeDoorLock : OutlineInteractable
{
    public GameObject door;

    private List<float> code = new List<float>() { -129.6f, 129.6f, -36f };

    private bool IsOpenable = true;
    private bool rotate = false;
    private bool solved = false;

    public float currentAngle = 0;
    private int stage = 0;
    private int rounds = 3;

    public override void Interact()
    {
        if (IsOpenable)
            rotate = true;
    }
    private void Update()
    {
        if (!rotate) return;

        if (Input.GetMouseButtonUp(0))
        {
            rotate = false;
            return;
        }
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = Player.MousePosition - pos;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        if (angle < 0) angle = 360 + angle;

        float angleDiff = angle - transform.rotation.eulerAngles.z;

        if (angleDiff > 100) angleDiff -= 360;
        if (angleDiff < -100) angleDiff += 360;

        currentAngle += angleDiff;
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);

        float startOpening = -360 * rounds;
        Debug.Log(stage);

        switch (stage)
        {
            case 0:
                if (currentAngle < startOpening) stage++;
                break;
            case 1:
                if (currentAngle < startOpening + code[0] + 3.6f)
                {
                    stage++;
                    rounds--;
                }
                break;
            case 2:
                if (currentAngle > startOpening + code[1] - 3.6f) stage++;
                break;
            case 3:
                if (currentAngle < startOpening + code[2] + 3.6f) solved = true;
                break;
        }

        if (solved)
        {
            Destroy(door.GetComponent<POIActivator>());   
            transform.parent.GetComponentInChildren<SafeDoorHand>().Unlock();
            IsOpenable = false;
            rotate = false;
        }

    }
}

