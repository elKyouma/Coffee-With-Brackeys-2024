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
    [SerializeField]
    private GameObject MetalPiece;
    [SerializeField]
    private float metalPieceOffset = 0.5f;

    [SerializeField]
    private SoundSO unlockSound;
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
            Unlock();
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

    public void Unlock()
    {
        Debug.Log("Unlocked");
        SoundManager.Instance.PlaySound(unlockSound, transform.position);
        LeanTween.moveY(MetalPiece, MetalPiece.transform.position.y + metalPieceOffset, 1f);
    }
}
