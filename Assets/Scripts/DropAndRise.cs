using UnityEngine;
using System.Collections;

public class DropAndRise : MonoBehaviour
{
    private Coroutine moveRoutine;
    private Vector3 initialPosition;
    private bool initialized = false;

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
