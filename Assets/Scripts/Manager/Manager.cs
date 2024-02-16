using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager s_instance;  // static을 통한 매니저 유일성 보장	
    public static Manager Instance { get { Init(); return s_instance; } }   // 유일성이 보장된 매니저 반환

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Manager>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Manager>();
        }
    }
}
