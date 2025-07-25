using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText; 

    private float deltaTime;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = $"FPS: {fps:F1}";
    }
}
