using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject quitCheckUI;
    public GameObject settingUI;

    private void Start()
    {
        // Play 버튼에 대한 클릭 이벤트 처리
        Button playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        if (playButton != null)
            playButton.onClick.AddListener(PlayClicked);

        // // Save 버튼에 대한 클릭 이벤트 처리
        // Button saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
        // saveButton.onClick.AddListener(SaveClicked);

        // Setting 버튼에 대한 클릭 이벤트 처리
        Button settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        settingButton.onClick.AddListener(SettingClicked);

        // Quit 버튼에 대한 클릭 이벤트 처리
        Button quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitClicked);   
        

    }

    // Play 버튼 클릭 시 동작
    void PlayClicked()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f;
    }

    // Setting 버튼 클릭 시 동작
    void SettingClicked()
    {
        // 종료 확인 UI를 활성화
        settingUI.SetActive(true);
        menuSet.SetActive(false);

        // Setting UI의 확인 버튼에 대한 클릭 이벤트 처리
        Button settingConfirmBtn = GameObject.Find("SettingConfirmButton").GetComponent<Button>();
        if (settingConfirmBtn != null)
            settingConfirmBtn.onClick.AddListener(SettingConfirmClicked);   
    }

    void SettingConfirmClicked()
    {
        settingUI.SetActive(false);
        menuSet.SetActive(true);
    }

    // Quit 버튼 클릭 시 동작
    void QuitClicked()
    {
        // 종료 확인 UI를 활성화
        quitCheckUI.SetActive(true);
        menuSet.SetActive(false);

        // Quit UI의 취소 버튼에 대한 클릭 이벤트 처리
        Button quitCancelBtn = GameObject.Find("QuitCancelButton").GetComponent<Button>();
        if (quitCancelBtn != null)
            quitCancelBtn.onClick.AddListener(QuitCancelClicked);

        // Quit UI의 확인 버튼에 대한 클릭 이벤트 처리
        Button quitConfirmBtn = GameObject.Find("QuitConfirmButton").GetComponent<Button>();
        if (quitConfirmBtn != null)
            quitConfirmBtn.onClick.AddListener(QuitConfirmClicked);   
    }

    void QuitCancelClicked()
    {
        quitCheckUI.SetActive(false);
        menuSet.SetActive(true);
    }

    void QuitConfirmClicked()
    {
        quitCheckUI.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }


}
