using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    public void SetMasterVolume(float level)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicVolume(float level)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
    public void SetSFXVolume(float level)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }
}
