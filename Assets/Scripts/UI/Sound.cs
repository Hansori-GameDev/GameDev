using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;
    public AudioMixerGroup audioMixerGroup; // 이 부분을 추가합니다.

    void Start()
    {
        audioMixerGroup = audioMixer.FindMatchingGroups("YourGroupName")[0];
    }

    public void AudioControl()
    {
        float sound = audioSlider.value;

        if(sound == -40f ) audioMixerGroup.audioMixer.SetFloat("BGM", -80); // 여기서 수정합니다.
        else audioMixerGroup.audioMixer.SetFloat("BGM", sound); // 여기서 수정합니다.
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}

