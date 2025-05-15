using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreenFade : MonoBehaviour
{
    [SerializeField] private float delayBeforeFade = 2f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private bool disableAfterFade = true;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;

        if (disableAfterFade)
            gameObject.SetActive(false);
    }
}
