using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioMixer musicMixer;

    [Header("Sliders")]
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        masterSlider.value = PlayerPrefs.GetFloat("AllVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    public void SetMasterVolume()
    {
        musicMixer.SetFloat("AllVolume", masterSlider.value);
        PlayerPrefs.SetFloat("AllVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        musicMixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        musicMixer.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
