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
        DataManager.instance.LoadData();
        SceneManager.LoadScene("Test_Player");
    }

    public void onClickExit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif

    }


    public void Test_onClickSave() {
        GameObject savePlayer = GameObject.Find("Player");

        Debug.Log("save" + savePlayer.transform.position.x.ToString() + savePlayer.transform.position.y.ToString());

        DataManager.instance.nowPlayer.PlayerPosX = savePlayer.transform.position.x;
        DataManager.instance.nowPlayer.PlayerPosY = savePlayer.transform.position.y;
        DataManager.instance.SaveData();
    }
}
