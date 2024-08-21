using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer MYmixer;
    [SerializeField] private Slider musicslider;
    [SerializeField] private Slider SFXslider;

    
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
            SetSFXVolume();
            SetSFXVolume();
        }
    }

    public void SetVolume()
    {
        float volume = musicslider.value;
        MYmixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }public void SetSFXVolume()
    {
        float volume = SFXslider.value;
        MYmixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadVolume()
    {
        musicslider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXslider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetVolume();
        SetSFXVolume();
    }
}
