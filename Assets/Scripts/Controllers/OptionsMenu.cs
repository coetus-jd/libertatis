using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer m_AudioMixer;
    public Dropdown m_ResolutionDropdown;
    private Resolution[] m_Resolutions;

    private void Start()
    {
        m_Resolutions = Screen.resolutions;
        m_ResolutionDropdown.ClearOptions();

        var options = new List<string>();
        foreach (var resolution in m_Resolutions)
        {
            var option = $"{resolution.width} x {resolution.height}";
            options.Add(option);
        }

        var currentResolution = $"{Screen.currentResolution.width} x {Screen.currentResolution.height}";
        m_ResolutionDropdown.AddOptions(options);
        m_ResolutionDropdown.value = options.FindIndex(resolution => resolution.Equals(currentResolution));
        m_ResolutionDropdown.RefreshShownValue();
    }    

    public void SetResolution(int resolutionIndex)
    {
        var resolution = m_Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolumeMusic(float music)
    {
        m_AudioMixer.SetFloat("music", music);
    }
    public void SetVolumeEffects(float effects)
    {
        m_AudioMixer.SetFloat("effects", effects);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
