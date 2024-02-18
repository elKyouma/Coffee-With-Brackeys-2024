using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneScript : OutlineInteractable
{
    private bool isRinging = false;
    private bool isPlaying = false;
    private SoundSO dialogue = null;

    [SerializeField] private Light diode;

    [SerializeField] AudioSource ringingSource;
    [SerializeField] AudioSource dialogueSource;

    public override void Interact()
    {
        if (isRinging)
        {
            isRinging = false;
            ringingSource.mute = true;
            dialogueSource.Play();
            diode.intensity = 0f;
        }
        else if (!isPlaying)
        {
            StartCoroutine(StartDialogue(dialogueSource.clip.length));
        }
    }
    public IEnumerator StartDialogue(float time = 0f)
    {
        isPlaying = true;
        dialogueSource.Play();
        yield return new WaitForSeconds(time);
        isPlaying = false;
    }
    public void StartRinging(SoundSO newDialogue, float offset = 0f)
    {
        dialogueSource.clip = newDialogue.audio;
        dialogueSource.volume = newDialogue.soundVolume;
        StartCoroutine(Wait(offset));
    }
    private void Update()
    {
        if (isRinging)
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
