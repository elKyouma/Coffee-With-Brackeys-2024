using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorWithPlanks : OutlineInteractable
{
    private enum DoorOpenings
    {
        right,
        left,
        both,
    }

    [SerializeField] DoorOpenings opening;
    [SerializeField, Range(0.2f, 1f)]
    private float openingSpeed = 0.5f;

    private float startAngle;
    private const float angles = 90;
    [Header("Planks")]
    [SerializeField] private int plankToDestroy = 7;

    private bool Openable => plankToDestroy <= 0;

    [SerializeField] private SoundSO openingSound;
    [SerializeField] private SoundSO closingSound;

    [Header("Key")]
    [Tooltip("If -1, no key is needed")]
    [SerializeField] private int keyId = -1;
    [SerializeField] private SoundSO usingKeySound;

    private void Start()
    {
        startAngle = transform.rotation.eulerAngles.y;
    }

    public void DeletePlank()
    {
        plankToDestroy--;

    }
    public override void Interact()
    {
        if (!Openable) return;
        if (keyId >= 0 && keyId != GameManager.Instance.HandObject.GetComponentInChildren<Key>().keyId) return;
        float rotateAngles = angles;
        Transform point = GetComponentInParent<Transform>();

        switch (opening)
        {
            case DoorOpenings.right:
                break;
            case DoorOpenings.left:
                rotateAngles = -rotateAngles;
                break;
            case DoorOpenings.both:
                if (Vector3.Dot(transform.forward, transform.position - GameManager.Instance.PlayerCharacter.position) < 0f)
                    rotateAngles = -rotateAngles;
                break;
            default:
                break;
        }

        if (transform.rotation.eulerAngles.y <= startAngle + angles * 0.5f)
        {
            point.LeanRotateY(startAngle + rotateAngles, openingSpeed);
            SoundManager.Instance.PlaySound(openingSound, transform.position);
        }
        else
        {
            point.LeanRotateY(startAngle, openingSpeed);
            SoundManager.Instance.PlaySound(closingSound, transform.position);
        }
    }
}
