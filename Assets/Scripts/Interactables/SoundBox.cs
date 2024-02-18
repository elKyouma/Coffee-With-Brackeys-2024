using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    private AudioSource source;

    public void Play(SoundSO sound, float offset)
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(SoundOffset(sound, offset));
        StartCoroutine(SoundBoxDestruction(sound.audio.length + offset));
    }

    IEnumerator SoundOffset(SoundSO sound, float time)
    {
        yield return new WaitForSeconds(time);
        source.volume = sound.soundVolume;
        source.PlayOneShot(sound.audio);
    }

    IEnumerator SoundBoxDestruction(float time)
    {
        yield return new WaitForSeconds(time + 0.1f);
        Destroy(gameObject);
    }
}

