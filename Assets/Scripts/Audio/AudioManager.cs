using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource sfxObject;
    [SerializeField] private AudioSource musicObject;
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
        AudioSource source = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        source.clip = audioClip;
        source.volume = volume;
        source.Play();
        Destroy(source.gameObject, source.clip.length);
    }
    public void PlayRandomSFX(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        AudioSource source = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
        source.clip = audioClip[Random.Range(0,audioClip.Length)];
        source.volume = volume;
        source.Play();
        Destroy(source.gameObject, source.clip.length);
    }
    public void PlayMusic(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource source = Instantiate(musicObject, spawnTransform.position, Quaternion.identity);
        source.clip = audioClip;
        source.volume = volume;
        source.loop = true;
        source.Play();
    }
}
