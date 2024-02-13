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
    public GameObject magnifier;
    public RenderTexture texture;
    private bool distortion = true;
    private bool inUse = false;


    public void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toPlayer = player.position - transform.position;
        float dot = Vector3.Dot(forward.normalized, toPlayer.normalized);

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
        if (!inUse)
        {
            Debug.Log("Using " + name);
            LeanTween.moveLocal(magnifier, new Vector3(-0.75f, 0.175f, -0.6f), 1);
            Resize(texture, 1000, 1000);
            inUse = true;
        }   
        else
        {
            LeanTween.moveLocal(magnifier, new Vector3(0f, 0f, 0f), 1);
            Resize(texture, 25, 25);
            inUse = false;
        }
    }

    void Resize(RenderTexture renderTexture, int width, int height)
    {
        if (renderTexture)
        {
            renderTexture.Release();
            renderTexture.width = width;
            renderTexture.height = height;
        }
    }
}