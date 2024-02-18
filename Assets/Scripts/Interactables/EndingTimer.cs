using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTimer : OutlineInteractable
{
    [SerializeField] private SoundSO counterSound;
    [SerializeField] private SoundSO rocketLaunchSound;
    [SerializeField] private AudioSource counterAudioSource;
    public void OnEnable()
    {
        StartCoroutine(EndGame());
    }
    public IEnumerator EndGame()
    {
        // Play counter sound
        counterAudioSource.clip = counterSound.audio;
        counterAudioSource.Play();
        yield return new WaitForSeconds(counterSound.audio.length);
        // End game
        counterAudioSource.clip = rocketLaunchSound.audio;
        counterAudioSource.Play();
    }
    public override void Interact()
    {
        counterAudioSource.mute = true;
        StopCoroutine(EndGame());
    }
}
