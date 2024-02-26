using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerData {
    public float PlayerPosX;
    public float PlayerPosY;
    // public int[] item = new int[6];
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData nowPlayer = new PlayerData();

    string path;
    string filename = "save";
    public bool isLoad = false;

    void Awake() {
        #region singleton setting
        if(instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/";
    }

    /***
        DataManager.Instance.SaveData()
    ***/
    public void SaveData() {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + filename, data);
    }

    /***
        DataManager.Instance.LoadData()
    ***/
    public void LoadData() {
        try {
            string data = File.ReadAllText(path + filename);
            Debug.Log(data);

            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            isLoad = true;
            Debug.Log(path+filename);
        } catch(Exception e) {
            Debug.Log(e);
        }
    }
}
