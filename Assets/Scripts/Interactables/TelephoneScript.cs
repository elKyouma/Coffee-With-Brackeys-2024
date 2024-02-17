using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneScript : OutlineInteractable
{
    private bool isRinging = true;
    private SoundSO dialogue = null;

    [SerializeField] private Light diode;

    [SerializeField] AudioSource ringingSource;

    public override void Interact()
    {
        if(isRinging)
        {
            isRinging = false;
            ringingSource.mute = true;
            SoundManager.Instance.PlaySound(dialogue, transform.position);
            diode.intensity = 0f;
        }
    }

    public void StartRinging(SoundSO newDialogue)
    {
        isRinging = true;
        dialogue = newDialogue;
        ringingSource.mute = false;
    }
    private void Update()
    {
        if(isRinging)
        {
            diode.intensity = Mathf.Sin(2 * Time.time) * 100f;
        }
    }

}
