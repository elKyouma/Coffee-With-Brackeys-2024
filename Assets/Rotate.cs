using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    private float Speed = 2f;

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * Speed * 5f, 0);
    }
}
