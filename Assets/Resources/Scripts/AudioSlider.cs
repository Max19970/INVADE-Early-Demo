using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string exposedMixerVariable;
    public string volume;

    public void UpdateValue(Slider slider)
    {
        volume = Mathf.Round(slider.value * 100).ToString();
        PlayerPrefs.SetFloat(exposedMixerVariable, slider.value);
    }

    public void ChangeVolume(Slider slider)
    {
        mixer.SetFloat(exposedMixerVariable, ConvertToDecibel(slider.value));
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat(exposedMixerVariable, ConvertToDecibel(value));
    }

    public float ConvertToDecibel(float value){
         return Mathf.Log10(Mathf.Max(value, 0.0001f))*20f;
    }

    public void ResetMe()
    {
        PlayerPrefs.SetFloat(exposedMixerVariable, 1f);
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(exposedMixerVariable);
        GetComponent<Slider>().onValueChanged.Invoke(PlayerPrefs.GetFloat(exposedMixerVariable));
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey(exposedMixerVariable)) PlayerPrefs.SetFloat(exposedMixerVariable, 1f);
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(exposedMixerVariable);
        GetComponent<Slider>().onValueChanged.Invoke(PlayerPrefs.GetFloat(exposedMixerVariable));
    }
}
