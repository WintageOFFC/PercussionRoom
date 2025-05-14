using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Controls audio mixer parameters through UI sliders.
/// </summary>
public class FXPanelManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider reverbSlider = null;
    [SerializeField] private Slider delaySlider = null;
    [SerializeField] private Slider pitchSlider = null;

    [SerializeField] private string reverbParamName = "Decay";
    [SerializeField] private string pitchParamName = "Pitch";
    [SerializeField] private string delayParamName = "Delay";
    [SerializeField] private string delayMixParamName = "Mix";

    /// <summary>
    /// Initializes the sliders and loads initial values from the audio mixer.
    /// </summary>
    private void Start()
    {
        LoadMixerValuesToSliders();

        if (reverbSlider != null)
            reverbSlider.onValueChanged.AddListener(SetDecay);

        if (delaySlider != null)
            delaySlider.onValueChanged.AddListener(SetDelay);

        if (pitchSlider != null)
            pitchSlider.onValueChanged.AddListener(SetPitch);
    }

    /// <summary>
    /// Loads current values from the audio mixer and sets slider values accordingly.
    /// </summary>
    private void LoadMixerValuesToSliders()
    {
        float value;

        if (reverbSlider != null && audioMixer.GetFloat(reverbParamName, out value))
            reverbSlider.value = value;

        if (delaySlider != null && audioMixer.GetFloat(delayParamName, out value))
            delaySlider.value = value;

        if (pitchSlider != null && audioMixer.GetFloat(pitchParamName, out value))
            pitchSlider.value = value * 10f;
    }

    /// <summary>
    /// Sets the decay (reverb) value in the audio mixer.
    /// </summary>
    /// <param name="value">The new decay value.</param>
    public void SetDecay(float value)
    {
        audioMixer.SetFloat(reverbParamName, value);
    }

    /// <summary>
    /// Sets the delay value in the audio mixer and toggles the mix parameter.
    /// </summary>
    /// <param name="value">The new delay value.</param>
    public void SetDelay(float value)
    {
        if (value <= 5f)
        {
            audioMixer.SetFloat(delayMixParamName, 0);
        }
        else
        {
            audioMixer.SetFloat(delayMixParamName, 1);
        }
        audioMixer.SetFloat(delayParamName, value);
    }

    /// <summary>
    /// Sets the pitch value in the audio mixer.
    /// </summary>
    /// <param name="value">The new pitch value (scaled by 0.1).</param>
    public void SetPitch(float value)
    {
        audioMixer.SetFloat(pitchParamName, value / 10f);
    }
}
