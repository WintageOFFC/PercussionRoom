using UnityEngine;
using System.Collections;

/// <summary>
/// Animates a small vertical drop and rise of the GameObject for visual feedback.
/// </summary>
public class DropAndRise : MonoBehaviour
{
    private Coroutine moveRoutine;
    private Vector3 initialPosition;
    private bool initialized = false;

    /// <summary>
    /// Starts the drop and rise animation. Resets if already running.
    /// </summary>
    public void DropRise()
    {
        if (!initialized)
        {
            initialPosition = transform.position;
            initialized = true;
        }

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        transform.position = initialPosition;

        moveRoutine = StartCoroutine(DropRiseRoutine());
    }

    /// <summary>
    /// Coroutine that moves the object down slightly and then back to its original position.
    /// </summary>
    private IEnumerator DropRiseRoutine()
    {
        Vector3 downPos = initialPosition - Vector3.up * 0.01f;
        float duration = 0.1f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(initialPosition, downPos, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(downPos, initialPosition, t);
            yield return null;
        }

        moveRoutine = null;
    }
}
