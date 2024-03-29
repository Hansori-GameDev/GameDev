using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerClickHandler
{
    public GameObject menuSet;

    public void OnPointerClick(PointerEventData eventData)
    {
        menuSet.SetActive(true);
        Time.timeScale = 0f;
    }
}
