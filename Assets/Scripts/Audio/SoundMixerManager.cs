using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        }
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }
    [SerializeField] private AudioMixer mainMixer;
    public void SetMasterVolume(float level)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }
    public void SetMusicVolume(float level)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }
    public void SetSFXVolume(float level)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }
}
