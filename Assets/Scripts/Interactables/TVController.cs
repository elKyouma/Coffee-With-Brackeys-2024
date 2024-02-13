using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    [SerializeField]
    private Transform knob1, knob2;
    [SerializeField]
    private VideoClip defaultVideo;
    [SerializeField]
    private ChannelVideo[] channels;

    private int channel;




}

[System.Serializable]
struct ChannelVideo
{
    [SerializeField]
    private int channel;
    [SerializeField]
    private VideoClip defaultVideo;
}
