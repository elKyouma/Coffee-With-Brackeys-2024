using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationBlockScript : MonoBehaviour

{
    public string value;
    private string[] cycleValues = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private int defaultIndex = 0;
    [SerializeField]
    private CombinationPadlockScript padlock;
    [SerializeField]
    private float rotationTime = 0.5f;
    [SerializeField]
    private float currentRotation;

    //Audio

    [SerializeField]
    private SoundSO rotateSound;

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
        SoundManager.Instance.PlaySound(rotateSound, transform.position);
        var deltaAngle = 360 / cycleValues.Length;
        Debug.Log("deltaAngle: " + deltaAngle);
        LeanTween.rotateLocal(gameObject, new Vector3(0, deltaAngle * currentIndex), rotationTime);
        // currentRotation = targetRotation;
        // yield return new WaitForSeconds(rotationTime);
    }
}
