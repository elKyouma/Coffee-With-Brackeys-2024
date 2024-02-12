using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField, Range(0, 1)] private float musicVolume = 1f;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField, Range(0, 1)] private float ambienceVolume = 1f;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField, Range(0, 1)] private float footStepVolume = 1f;

    [SerializeField] private GameObject soundBoxPrefab;
    private GameObject soundBox;



    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        musicSource.volume = musicVolume;
        ambienceSource.volume = ambienceVolume;
        footStepSource.volume = footStepVolume;
    }

    public void PlaySound(SoundSO sound, Vector3 soundPosition, float offset = 0)
    {
        soundBox = Instantiate(soundBoxPrefab, soundPosition, Quaternion.identity, gameObject.transform);
        soundBox.GetComponent<SoundBox>().Play(sound, offset);
    }

    public void MuteFootsteps(bool active)
    {
        footStepSource.mute = active;
    }


}
