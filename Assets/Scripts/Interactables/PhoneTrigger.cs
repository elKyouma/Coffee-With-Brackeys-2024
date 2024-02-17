using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTrigger : MonoBehaviour
{
    [SerializeField] private SoundSO dialogue;
    [SerializeField] private GameObject phoneTrigger;
    [SerializeField] private TelephoneScript telephone;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!hasTriggered && phoneTrigger == other.gameObject)
        {
            telephone.StartRinging(dialogue, 10f);
            hasTriggered = true;
        }
    }

    
}
