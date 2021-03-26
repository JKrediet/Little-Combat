using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider vfxSlider;

    public TMP_Dropdown qualityDropDown;

    public AudioMixer vfxMixer;
    public AudioMixer musicMixer;

    public TMP_Dropdown resolutionsDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        musicSlider.value = GetMusicLevel();
        vfxSlider.value = GetVFXLevel();

        qualityDropDown.value = QualitySettings.GetQualityLevel();
        qualityDropDown.RefreshShownValue();

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + "@" + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolution;
        resolutionsDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        vfxMixer.SetFloat("volume", vfxSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", musicSlider.value);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public float GetMusicLevel()
    {
        float value;
        bool result = musicMixer.GetFloat("volume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }

    public float GetVFXLevel()
    {
        float value;
        bool result = vfxMixer.GetFloat("volume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
}
