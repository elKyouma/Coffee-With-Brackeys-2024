using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : OutlineInteractable
{
    [SerializeField, Range(0.1f, 0.8f)] private float range;
    [SerializeField, Range(0.1f, 0.8f)] private float speed;
    private float startPosZ;

    [SerializeField] private SoundSO openingSound;
    [SerializeField] private SoundSO closingSound;

    private void Awake()
    {
        startPosZ = transform.localPosition.z;
    }

    public override void Interact()
    {
        if (transform.localPosition.z <= startPosZ + range * 0.5f)
        {
            SoundManager.Instance.PlaySound(openingSound, gameObject.transform.position);
            speed = openingSound.audio.length;
            transform.LeanMoveLocalZ(startPosZ + range, speed).setEaseInOutQuad();
        }
        else
        {
            SoundManager.Instance.PlaySound(closingSound, gameObject.transform.position);
            speed = closingSound.audio.length;
            transform.LeanMoveLocalZ(startPosZ, speed).setEaseInOutQuad();
        }
    }
}
