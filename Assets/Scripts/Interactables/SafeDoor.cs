using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoor : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0.2f, 1f)]
    private float openingSpeed = 0.5f;

    private MeshRenderer meshRenderer;
    private float startAngle;
    private const float angles = 120;
    private const string amplitudeParam = "_Amplitude";
    private bool openable = false;

    [SerializeField] private SoundSO openingSound;
    [SerializeField] private SoundSO closingSound;

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

    public void Unlock()
    {
        openable = true;
    }
        
    public void Interact()
    {
        if (!openable) return;
        float rotateAngles = -angles;
        Transform point = GetComponentInParent<Transform>();

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

    public void Selected()
    {
        meshRenderer.material.SetFloat(amplitudeParam, 0.5f);

    }

    public void Unselected()
    {
        meshRenderer.material.SetFloat(amplitudeParam, 0.0f);

    }
}
