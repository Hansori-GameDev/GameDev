using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject menuSet;

    private void Start()
    {
        // Play 버튼에 대한 클릭 이벤트 처리
        Button playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        playButton.onClick.AddListener(PlayClicked);
        
    }

    // Play 버튼 클릭 시 동작
    void PlayClicked()
    {
        menuSet.SetActive(false);
        Time.timeScale = 1f;
    }


}
