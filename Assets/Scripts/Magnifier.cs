using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Magnifier : Item, IInteractable
{
    public bool camera1 = false;
    public bool camera2 = false;
    public Transform player;
    private bool distortion = true;


    public void Update()
    {
        var forward = transform.forward;
        var toPlayer = player.position - transform.position;
        float angle = Vector3.Dot(forward, toPlayer);
        Debug.Log(angle);
    }

    public override void UseItem()
    {
        Debug.Log("Using " + name);
    }
}