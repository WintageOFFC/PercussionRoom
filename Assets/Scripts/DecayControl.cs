using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DecayControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; 
    [SerializeField] private Slider decaySlider;     
    [SerializeField] private string exposedParamName = "Decay"; 

    void Start()
    {
        if (decaySlider != null)
        {
            decaySlider.onValueChanged.AddListener(SetDecay);
        }
    }

    public void SetDecay(float value)
    {
        audioMixer.SetFloat(exposedParamName, value);
    }
}
