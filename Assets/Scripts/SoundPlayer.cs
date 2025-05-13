using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private AudioMixerGroup outputMixerGroup; 

    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null) return;

        GameObject audioObj = new GameObject("OneShotAudio");
        audioObj.transform.position = transform.position;

        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = outputMixerGroup;
        source.Play();

        Destroy(audioObj, lifetime);
    }
}
