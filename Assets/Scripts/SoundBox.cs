using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    private AudioSource source;

    public void Play(SoundSO sound, float offset)
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(SoundOffset(offset));
        source.volume = sound.soundVolume;
        source.PlayOneShot(sound.audio);
        StartCoroutine(SoundBoxDestruction(sound.audio.length));
    }

    IEnumerator SoundOffset(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator SoundBoxDestruction(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

