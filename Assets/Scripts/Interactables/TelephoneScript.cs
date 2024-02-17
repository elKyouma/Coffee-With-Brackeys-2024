using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneScript : OutlineInteractable
{
    private bool isRinging = false;
    private SoundSO dialogue = null;

    [SerializeField] private Light diode;

    [SerializeField] AudioSource ringingSource;
    [SerializeField] AudioSource dialogueSource;

    public override void Interact()
    {
        if(isRinging)
        {
            isRinging = false;
            ringingSource.mute = true;
            dialogueSource.Play();
            diode.intensity = 0f;
        }
    }

    public void StartRinging(SoundSO newDialogue, float offset = 0f)
    {
        dialogueSource.clip = newDialogue.audio;
        dialogueSource.volume = newDialogue.soundVolume;
        StartCoroutine(Wait(offset));
    }
    private void Update()
    {
        if(isRinging)
        {
            diode.intensity = Mathf.Sin(2 * Time.time);
        }
    }
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        isRinging = true;
        ringingSource.mute = false;
    }

}
