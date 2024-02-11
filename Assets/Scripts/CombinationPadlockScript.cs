using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombinationPadlockScript : MonoBehaviour
{
    public bool isLocked = true;
    [SerializeField]
    private string combination = "1234";
    private string input = "";
    private CombinationBlockScript[] blocks;
    [SerializeField]
    private string[] blockValues;

    void Start()
    {
        blocks = GetComponentsInChildren<CombinationBlockScript>();
        blockValues = new string[blocks.Length];
    }
    public void CheckCombination()
    {
        input = "";
        for (int i = 0; i < blocks.Length; i++)
        {
            blockValues[i] = blocks[i].value;
            input += blockValues[i];
        }
        if (input == combination)
        {
            isLocked = false;
            Debug.Log("Unlocked");
        }
        else
        {
            isLocked = true;
        }
    }
    public void Reset()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].Reset();
            blockValues[i] = blocks[i].value;
        }
        isLocked = true;
    }

}
