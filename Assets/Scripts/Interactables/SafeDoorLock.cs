using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;
using UnityEngine.UIElements;
using System.Security.Permissions;
using System.Xml.Serialization;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;

public class SafeDoorLock : OutlineInteractable
{
    public GameObject door;

    private List<float> code = new List<float>() { -129.6f, 489.6f, 324f };

    private bool IsOpenable = true;
    private bool rotate = false;
    private bool solved = false;

    public float currentAngle = 0;
    public int stage = 0;

    [SerializeField] private SoundSO stageCompete;

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
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        float startOpening = -360 * 3;

        switch (stage)
        {
            case 0:
                if (currentAngle < startOpening + code[0] + 3.6f) StageComplete();
                break;
            case 1:
                if (currentAngle > startOpening + code[1] - 3.6f) StageComplete();
                if (currentAngle < startOpening + code[0] - 7.2f) OpeningFailed();
                break;
            case 2:
                if (currentAngle < startOpening + code[2] + 3.6f) StageComplete();
                if (currentAngle > startOpening + code[1] + 7.2f) OpeningFailed();
                break;
        }

        if (solved)
        {
            Destroy(door.GetComponent<POIActivator>());   
            transform.parent.GetComponentInChildren<SafeDoorHand>().Unlock();
            IsOpenable = false;
            rotate = false;
            GameManager.Instance.ExitPuzzleMode();
        }

    }

    private void StageComplete()
    {
        stage++;
        if (stage > 2) solved = true;
    }

    public void OpeningFailed()
    {
        currentAngle = 0;
        stage = 0;
        rotate = false;
        LeanTween.rotateLocal(gameObject, new Vector3(0, 0, 0), 0.5f);
    }
}

