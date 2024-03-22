using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerClickHandler
{
    public GameObject menuSet;
    bool isMenuActive = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        isMenuActive = !isMenuActive;
        menuSet.SetActive(isMenuActive);

        Time.timeScale = isMenuActive ? 0f : 1f;
        Debug.Log($"{isMenuActive}, {Time.timeScale}");
    }
}
