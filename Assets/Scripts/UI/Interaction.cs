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
            // ��ġ �Է��� �߻��ϸ� onClick �̺�Ʈ ȣ��
            Debug.Log("Ŭ���߽��ϴ�!");
        }

    }
}
