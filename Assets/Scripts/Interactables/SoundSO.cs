using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSound", menuName = "Sound")]
public class SoundSO : ScriptableObject
{
    [SerializeField] public AudioClip audio;
    [SerializeField] public float soundVolume = 1;
}
