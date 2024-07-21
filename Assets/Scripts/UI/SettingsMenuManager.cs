using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenuManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer audioMixer;
    // Start is called before the first frame update

    public void ChangeGraficsQuality()
    {
        QualitySettings.SetQualityLevel(resolutionDropdown.value);

    }

    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {

        audioMixer.SetFloat("SfxVol", sfxVol.value);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
