using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField, Range(0, 1)] public float musicVolume = 1f;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField, Range(0, 1)] public float ambienceVolume = 1f;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField, Range(0, 1)] private float footStepVolume = 1f;

    [SerializeField] private GameObject soundBoxPrefab;
    private GameObject soundBox;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private AudioMixer masterMixer;


    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("MasterVolume", 1));
        musicSource.volume = musicVolume;
        ambienceSource.volume = ambienceVolume;
        footStepSource.volume = footStepVolume;
    }

    public void PlaySound(SoundSO sound, Vector3 soundPosition, float offset = 0f)
    {
        soundBox = Instantiate(soundBoxPrefab, soundPosition, Quaternion.identity, gameObject.transform);
        soundBox.GetComponent<SoundBox>().Play(sound, offset);
    }

    public void MuteFootsteps(bool active)
    {
        footStepSource.mute = active;
    }

    public void RefreshSliders(float music, float ambience)
    {
        masterSlider.value = music;
    }
    public void SetVolume(float volume)
    {
        if (volume < 0.0001f)
            volume = 0.0001f;
        RefreshSliders(volume, volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        masterMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    public void SetVolumeFromSlider()
    {
        SetVolume(masterSlider.value);
    }
}
