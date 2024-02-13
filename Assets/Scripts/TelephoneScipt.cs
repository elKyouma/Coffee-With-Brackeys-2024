using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneScipt : OutlineInteractable
{
    private bool isRinging = false;
    private SoundSO dialogue = null;

    [SerializeField] private Light diode;

    public override void Interact()
    {
        if(isRinging)
        {
            Debug.Log("kurwa");
            isRinging = false;
            SoundManager.Instance.PlaySound(dialogue, transform.position);
            diode.intensity = 0f;
        }
    }

    public void StartRinging(SoundSO newDialogue)
    {
        isRinging = true;
        dialogue = newDialogue;
    }
    private void Update()
    {
        if(isRinging)
        {
            diode.intensity = Mathf.Sin(Time.time) * 100f;
        }
    }
}
