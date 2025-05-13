using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SmoothAudioPlayer : MonoBehaviour
{
    [SerializeField] private float fadeOutDuration = 0.05f;
    private AudioSource audioSource;
    private Coroutine currentFade;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
            audioSource.volume = 1f;
        }

        currentFade = StartCoroutine(FadeOutAndPlay());
    }

    private IEnumerator FadeOutAndPlay()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeOutDuration;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.Play();

        currentFade = null;
    }
}
