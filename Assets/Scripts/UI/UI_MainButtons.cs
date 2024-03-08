using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainButtons : MonoBehaviour
{
    
    public void onClickNewGame() {
        SceneManager.LoadScene("Scene_0");
    }

    public void onClickContinue() {
        Manager.Data.LoadData();
        SceneManager.LoadScene("Scene_0");
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

        Manager.Data.nowPlayer.PlayerPosX = savePlayer.transform.position.x;
        Manager.Data.nowPlayer.PlayerPosY = savePlayer.transform.position.y;
        Manager.Data.SaveData();
    }
}
