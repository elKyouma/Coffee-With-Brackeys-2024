using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    private float stress = 0;
    private Vector3 _lastPosition;
    private Vector3 _lastRotation;
    [Tooltip("Exponent for calculating the shake factor. Useful for creating different effect fade outs")]
    public float TraumaExponent = 0.5f;
    [Tooltip("Maximum angle that the gameobject can shake. In euler angles.")]
    public Vector3 MaximumAngularShake = Vector3.one * 5;
    [Tooltip("Maximum translation that the gameobject can receive when applying the shake effect.")]
    public Vector3 MaximumTranslationShake = Vector3.one * 0.25f;
    public AnimationCurve curve;

    private float time, lentgh, amp;

    private void Update()
    {
        stress = amp * curve.Evaluate((lentgh - time)/lentgh);
        if (time > 0)
        {
            var previousRotation = _lastRotation;
            var previousPosition = _lastPosition;

            _lastPosition = new Vector3(
                MaximumTranslationShake.x * (Mathf.PerlinNoise(0, Time.time * 25) * 2 - 1),
                MaximumTranslationShake.y * (Mathf.PerlinNoise(1, Time.time * 25) * 2 - 1),
                MaximumTranslationShake.z * (Mathf.PerlinNoise(2, Time.time * 25) * 2 - 1)
            ) * stress;

            _lastRotation = new Vector3(
                MaximumAngularShake.x * (Mathf.PerlinNoise(3, Time.time * 25) * 2 - 1),
                MaximumAngularShake.y * (Mathf.PerlinNoise(4, Time.time * 25) * 2 - 1),
                MaximumAngularShake.z * (Mathf.PerlinNoise(5, Time.time * 25) * 2 - 1)
            ) * stress;

            transform.localPosition += _lastPosition - previousPosition;
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + _lastRotation - previousRotation);
            time -= Time.deltaTime;
            return;
        }

        if (_lastPosition == Vector3.zero && _lastRotation == Vector3.zero) return;
        /* Clear the transform of any left over translation and rotations */
        transform.localPosition -= _lastPosition;
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles - _lastRotation);
        _lastPosition = Vector3.zero;
        _lastRotation = Vector3.zero;

    }

    public void ShakeOn(float newTime, float amp)
    {
        this.amp = amp;
        lentgh = newTime;
        time = newTime;
    }

}
