using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockClock : MonoBehaviour
{
    [SerializeField] private ClockPuzzle clock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ClockUnlock")
        {
            clock.enabled = true;
            Destroy(this);
        }
    }
}
