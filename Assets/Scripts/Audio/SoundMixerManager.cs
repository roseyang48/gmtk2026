using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
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
