using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTimer : OutlineInteractable
{
    [SerializeField] private SoundSO counterSound;
    [SerializeField] private SoundSO rocketLaunchSound;
    [SerializeField] private AudioSource counterAudioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraShaker cameraShaker;
    private bool lost;
    public void OnEnable()
    {
        StartCoroutine(EndGame());
        animator.SetBool("isRunning", true);
    }
    public IEnumerator EndGame()
    {
        // Play counter sound
        counterAudioSource.clip = counterSound.audio;
        counterAudioSource.Play();
        yield return new WaitForSeconds(counterSound.audio.length);
        // End game
        lost = true;
        counterAudioSource.clip = rocketLaunchSound.audio;
        counterAudioSource.Play();
        // Screen shake 
        cameraShaker.ShakeOn(1f, 5f);
    }
    public override void Interact()
    {
        if (lost) return;
        counterAudioSource.mute = true;
        StopCoroutine(EndGame());
    }
}
