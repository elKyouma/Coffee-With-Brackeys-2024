using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject longHand;
    [SerializeField] private GameObject shortHand;
    private float longHandPosition;
    private float shortHandPosition;

    [SerializeField] private float longHandSolution;
    [SerializeField] private float shortHandSolution;

    [SerializeField] private MovablePainting painting;

    [SerializeField] private SoundSO solveSound;

    bool isSolved = false;

    void Update()
    {
        longHandPosition = AdjustHandPosition(longHand);
        shortHandPosition = AdjustHandPosition(shortHand);
        Debug.Log("short hand position:" + shortHandPosition);
        if (Mathf.Abs(longHandPosition - longHandSolution) < 10f)
        {
            if(Mathf.Abs(shortHandPosition - shortHandSolution) < 10f)
            {
                painting.MovePainting();
                if(!isSolved)
                    SoundManager.Instance.PlaySound(solveSound, longHand.transform.position);
                isSolved = true;
            }
        }
    }

    private float AdjustHandPosition(GameObject hand)
    {
        float newPosition = hand.transform.eulerAngles.x;
        //Debug.Log(newPosition);
        newPosition -= 270f;
        if (newPosition < 0)
            newPosition += 360f;
        

        return newPosition;
    }
}
