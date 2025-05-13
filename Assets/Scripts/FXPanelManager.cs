using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FXPanelManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider reverbSlider = null;
    [SerializeField] private Slider chorusSlider = null;
    [SerializeField] private Slider pitchSlider = null;
    [SerializeField] private string reverbParamName = "Decay";
    [SerializeField] private string pitchParamName = "Pitch";

    void Start()
    {
        if (reverbSlider != null && pitchSlider != null)
        {
            reverbSlider.onValueChanged.AddListener(SetDecay);
            pitchSlider.onValueChanged.AddListener(SetPitch);
        }
    }

    public void SetDecay(float value)
    {
        audioMixer.SetFloat(reverbParamName, value);
    }
    public void SetPitch(float value)
    {
        audioMixer.SetFloat(pitchParamName, value/10);
    }
}
