using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithPlanks : OutlineInteractable
{
    [SerializeField, Range(0.2f, 1f)]
    private float openingSpeed = 0.5f;

    private float startAngle;
    private const float angles = 90;
    [SerializeField]
    private int plankToDestroy = 7;
    private bool Openable => plankToDestroy <= 0;

    [SerializeField] private SoundSO openingSound;
    [SerializeField] private SoundSO closingSound;

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
        float rotateAngles = angles;
        Transform point = GetComponentInParent<Transform>();
        if (Vector3.Dot(transform.forward, transform.position - GameManager.Instance.PlayerCharacter.position) < 0f)
            rotateAngles = -rotateAngles;

        if (transform.rotation.eulerAngles.y == startAngle)
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
