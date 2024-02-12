using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    private AudioSource source;

    public void PlayOneClip(AudioClip sound)
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(sound);
        StartCoroutine(SoundBoxDestruction(sound.length));
    }

    IEnumerator SoundBoxDestruction(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

