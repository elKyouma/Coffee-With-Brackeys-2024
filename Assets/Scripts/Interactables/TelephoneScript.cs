using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TelephoneScript : OutlineInteractable
{
    private bool isRinging = false;
    private bool isPlaying = false;
    [SerializeField] private SoundSO dialogue = null;

    [SerializeField] private Light diode;

    [SerializeField] AudioSource ringingSource;
    [SerializeField] AudioSource dialogueSource;
    [Header("Dialogue Subtitles")]
    [SerializeField] private TextMeshProUGUI subtitleText;
    public override void Interact()
    {
        if (isRinging)
        {
            isRinging = false;
            ringingSource.mute = true;
            StartCoroutine(ShowSubtitles(dialogueSource.clip.length));
            dialogueSource.Play();
            diode.intensity = 0f;
        }
        else if (!isPlaying)
        {
            StartCoroutine(StartDialogue(dialogueSource.clip.length));
            StartCoroutine(ShowSubtitles(dialogueSource.clip.length));
        }
    }
    public IEnumerator ShowSubtitles(float time = 0f)
    {
        subtitleText.text = dialogue.subtitle;
        yield return new WaitForSeconds(time);
        subtitleText.text = "";
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
        dialogue = newDialogue;
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
