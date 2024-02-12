using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private GameObject soundBoxPrefab;
    private GameObject soundBox;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlaySound(AudioClip sound, Vector3 soundPosition)
    {
        soundBox = Instantiate(soundBoxPrefab, soundPosition, Quaternion.identity);
        soundBox.GetComponent<SoundBox>().PlayOneClip(sound);
    }


}
