using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoor : MonoBehaviour
{
    [SerializeField, Range(0.2f, 1f)]
    private float openingSpeed = 0.5f;

    private MeshRenderer meshRenderer;
    private float startAngle;
    private const float angles = 120;
    private const string amplitudeParam = "_Amplitude";

    [SerializeField] private SoundSO openingSound;
    [SerializeField] private SoundSO closingSound;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.SetFloat(amplitudeParam, 0.0f);
        startAngle = transform.rotation.eulerAngles.y;
    }
        
    public void Open()
    {
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
}
