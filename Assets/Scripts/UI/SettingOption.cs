using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingOption : MonoBehaviour
{
    Resolution[] resolutions;
    public Toggle fullscreenBtn;
    public TMP_Dropdown resolutionDropdown;
    [SerializeField] private int resolutionNum;

    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        resolutions = Screen.resolutions;

        HashSet<string> uniqueResolutions = new HashSet<string>();

        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            string resolutionString = item.width + "x" + item.height + " " + item.refreshRate + "hz";

            if (!uniqueResolutions.Contains(resolutionString))
            {
                uniqueResolutions.Add(resolutionString);

                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(resolutionString);
                dropdownOptions.Add(option);

                if (item.width == Screen.width && item.height == Screen.height)
                    resolutionDropdown.value = optionNum;
                optionNum++;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(dropdownOptions);
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        Screen.fullScreenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OkBtnClick()
    {
        if (resolutions.Length > resolutionNum && resolutionNum >= 0)
        {
            Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            Screen.fullScreenMode);
        }
        else
        {
            Debug.LogError("Invalid resolution index");
        }
    }
}
