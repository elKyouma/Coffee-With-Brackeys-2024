using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationBlockScript : MonoBehaviour
{
    public string value;
    private string defaultValue = "0";
    [SerializeField]
    private CombinationPadlockScript padlock;
    void Start()
    {
        padlock = transform.parent.GetComponent<CombinationPadlockScript>();
    }

    void OnMouseDown()
    {
        if (value == "9")
        {
            value = "0";
        }
        else
        {
            value = (int.Parse(value) + 1).ToString();
        }
        padlock.CheckCombination();

    }
    public void Reset()
    {
        value = defaultValue;
    }
}
