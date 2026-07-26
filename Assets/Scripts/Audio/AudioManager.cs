using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource musicObject;
    private List<AudioSource> SFXGroup = new List<AudioSource>();
    private AudioSource currMusic;
    [SerializeField] int maxPolyphony;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void PlaySFX(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if(SFXGroup.Count < maxPolyphony)
        {
            AudioSource source = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
            source.clip = audioClip;
            source.volume = volume;
            source.Play();
            SFXGroup.Add(source);
            StartCoroutine(RemoveSource(source.clip.length, source));
            Destroy(source.gameObject, source.clip.length + 0.1f);
        }
    }
    public void PlayRandomSFX(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        if(SFXGroup.Count < maxPolyphony)
        {
            AudioSource source = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
            source.clip = audioClip[Random.Range(0,audioClip.Length)];
            source.volume = volume;
            source.Play();
            SFXGroup.Add(source);
            StartCoroutine(RemoveSource(source.clip.length, source));
            Destroy(source.gameObject, source.clip.length + 0.1f);
        }
    }
    public void PlayMusic(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if(currMusic != null)
        {
            currMusic.Stop();
            Destroy(currMusic.gameObject);
        }
        AudioSource source = Instantiate(musicObject, spawnTransform.position, Quaternion.identity);
        source.clip = audioClip;
        source.volume = volume;
        source.loop = true;
        currMusic = source;
        source.Play();
    }
    private IEnumerator RemoveSource(float time, AudioSource obj)
    {
        yield return new WaitForSeconds(time);
        SFXGroup.Remove(obj);
    }
}
