using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnifier : Item, IInteractable
{
    public Transform camera1;
    public Transform player;
    private bool distortion = true;


    public void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toPlayer = player.position - transform.position;
        float dot = Vector3.Dot(forward.normalized, toPlayer.normalized);

        Debug.Log(dot);
        Debug.Log(camera1.localEulerAngles);
        if (dot > 0 && camera1.localEulerAngles.x < 180)
        {
            camera1.Rotate(180, 0, 0);
            camera1.position.Set(camera1.position.x, camera1.position.y * -1, camera1.position.z);
        }
        if (dot < 0 && camera1.localEulerAngles.x > 180)
        {
            camera1.Rotate(180, 0, 0);
            camera1.position.Set(camera1.position.x, camera1.position.y * -1, camera1.position.z);
        }
    }

    public override void UseItem()
    {
        Debug.Log("Using " + name);
    }
}