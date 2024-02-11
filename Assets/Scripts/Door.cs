using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0.2f, 1f)]
    private float openingSpeed = 0.5f;

    private MeshRenderer meshRenderer;
    private float startAngle;
    private const float angles = 90;
    private const string amplitudeParam = "_Amplitude";

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.SetFloat(amplitudeParam, 0.0f);
        startAngle = transform.rotation.eulerAngles.y;
    }

    private void OnEnable()
    {
        Interactor.AddInteractable(transform);
    }

    private void OnDisable()
    {
        Interactor.DeleteInteractable(transform);
    }
    public void Interact()
    {
        float rotateAngles = angles;
        if(Vector3.Dot(transform.forward, transform.position - GameManager.Instance.Player.position) > 0f)
            rotateAngles = -rotateAngles;

        if(transform.rotation.eulerAngles.y == startAngle)
            transform.LeanRotateY(startAngle + rotateAngles, openingSpeed);
        else
            transform.LeanRotateY(startAngle, openingSpeed);
    }

    public void Selected()
    {
        meshRenderer.material.SetFloat(amplitudeParam, 0.5f);

    }

    public void Unselected()
    {
        meshRenderer.material.SetFloat(amplitudeParam, 0.0f);

    }
}
