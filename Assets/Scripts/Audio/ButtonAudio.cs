using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public void PlayAudio(AudioClip clip)
    {
        AudioManager.Instance.PlaySFX(clip, transform, 1f);
    }
}
