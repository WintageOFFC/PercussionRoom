using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Plays one-shot audio clips with fade-out handling when the same sound is triggered again.
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private float fadeOutDuration = 0.1f;
    [SerializeField] private AudioMixerGroup outputMixerGroup;

    private Dictionary<AudioClip, GameObject> activeSounds = new Dictionary<AudioClip, GameObject>();

    /// <summary>
    /// Plays a one-shot audio clip. If the same clip is already playing, it fades out and is replaced.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null) return;

        if (activeSounds.TryGetValue(clip, out GameObject oldObj) && oldObj != null)
        {
            StartCoroutine(FadeOutAndDestroy(oldObj));
        }

        GameObject audioObj = new GameObject($"OneShotAudio_{clip.name}");
        audioObj.transform.position = transform.position;

        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = outputMixerGroup;
        source.Play();

        activeSounds[clip] = audioObj;

        Destroy(audioObj, lifetime);
    }

    /// <summary>
    /// Coroutine that fades out the audio source volume and destroys the object.
    /// </summary>
    /// <param name="audioObj">The GameObject with an AudioSource to fade and destroy.</param>
    private IEnumerator FadeOutAndDestroy(GameObject audioObj)
    {
        if (audioObj == null) yield break;

        AudioSource source = audioObj.GetComponent<AudioSource>();
        if (source == null) yield break;

        float startVolume = source.volume;
        float t = 0f;

        while (t < fadeOutDuration)
        {
            if (source == null) yield break;

            t += Time.deltaTime;
            float normalized = t / fadeOutDuration;
            source.volume = Mathf.Lerp(startVolume, 0f, normalized);
            yield return null;
        }

        if (source != null) source.Stop();

        if (audioObj != null)
            Destroy(audioObj);
    }
}
