using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interaction : MonoBehaviour, IPointerDownHandler
{
    public bool isInteractable = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isInteractable)
        {
            // 터치 입력이 발생하면 onClick 이벤트 호출
            Debug.Log("클릭했습니다!");
        }

    }
}
