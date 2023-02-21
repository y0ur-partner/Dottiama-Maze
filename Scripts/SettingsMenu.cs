using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int ci = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width +
            " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                ci = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = ci;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int ri)
    {
        Resolution res = resolutions[ri];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}