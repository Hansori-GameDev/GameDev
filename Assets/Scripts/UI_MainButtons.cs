using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainButtons : MonoBehaviour
{
    
    public void onClickNewGame() {
        SceneManager.LoadScene("Test_Player");
    }

    public void onClickContinue() {

    }

    public void onClickExit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif

    }
}
