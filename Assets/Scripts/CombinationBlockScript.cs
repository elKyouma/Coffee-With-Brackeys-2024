using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationBlockScript : MonoBehaviour

{
    public string value;
    private string[] cycleValues = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    private int currentIndex = 0;
    [SerializeField]
    private int defaultIndex = 0;
    [SerializeField]
    private CombinationPadlockScript padlock;
    [SerializeField]
    private float rotationTime = 0.5f;

    void OnMouseDown()
    {
        AddValue();
    }

    public void AddValue()
    {
        currentIndex = (currentIndex + 1) % cycleValues.Length;
        value = cycleValues[currentIndex];
        padlock.CheckCombination();
        Rotate();
    }
    public void Reset()
    {
        value = cycleValues[defaultIndex];
        currentIndex = defaultIndex;
    }
    public void Rotate()
    {
        var deltaAngle = 360 / cycleValues.Length;
        var currentRotation = transform.rotation.eulerAngles.y;
        transform.LeanRotateY(currentRotation + deltaAngle, rotationTime);
    }
}
