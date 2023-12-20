using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPage : MonoBehaviour
{
    // public float masterVolume;
    // public float musicVolume;
    // public float sfxVolume;

    // private AudioSlider masterSlider;
    // private AudioSlider musicSlider;
    // private AudioSlider sfxSlider;


    // public Dictionary<string, string> GetData()
    // {
    //     return new Dictionary<string, string>()
    //     {
    //         {"masterVolume", masterVolume.ToString().Replace(",", ".")},
    //         {"musicVolume", musicVolume.ToString().Replace(",", ".")},
    //         {"sfxVolume", sfxVolume.ToString().Replace(",", ".")}
    //     };
    // }

    // public void LoadData(Dictionary<string, string> data)
    // {
    //     Debug.Log(float.Parse(data["masterVolume"], CultureInfo.InvariantCulture.NumberFormat));
    //     masterSlider.gameObject.GetComponent<Slider>().value = float.Parse(data["masterVolume"], CultureInfo.InvariantCulture.NumberFormat);
    //     musicSlider.gameObject.GetComponent<Slider>().value = float.Parse(data["musicVolume"], CultureInfo.InvariantCulture.NumberFormat);
    //     sfxSlider.gameObject.GetComponent<Slider>().value = float.Parse(data["sfxVolume"], CultureInfo.InvariantCulture.NumberFormat);
    //     masterVolume = masterSlider.gameObject.GetComponent<Slider>().value;
    //     musicVolume = musicSlider.gameObject.GetComponent<Slider>().value;
    //     sfxVolume = sfxSlider.gameObject.GetComponent<Slider>().value;
    //     transform.Find("Master Volume").transform.Find("Slider").GetComponent<AudioSlider>().actualVolume = masterSlider.gameObject.GetComponent<Slider>().value;
    //     transform.Find("Music Volume").transform.Find("Slider").GetComponent<AudioSlider>().actualVolume = musicSlider.gameObject.GetComponent<Slider>().value;
    //     transform.Find("SFX Volume").transform.Find("Slider").GetComponent<AudioSlider>().actualVolume = sfxSlider.gameObject.GetComponent<Slider>().value;
    //     gameObject.SetActive(false);
    // }


    // void Awake()
    // {
    //     masterSlider = transform.Find("Master Volume").transform.Find("Slider").GetComponent<AudioSlider>();
    //     musicSlider = transform.Find("Music Volume").transform.Find("Slider").GetComponent<AudioSlider>();
    //     sfxSlider = transform.Find("SFX Volume").transform.Find("Slider").GetComponent<AudioSlider>();
    // }

    // void Update()
    // {
    //     masterVolume = masterSlider.actualVolume;
    //     musicVolume = musicSlider.actualVolume;
    //     sfxVolume = sfxSlider.actualVolume;
    // }
}
