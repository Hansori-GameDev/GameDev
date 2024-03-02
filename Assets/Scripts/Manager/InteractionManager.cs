using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager
{
    // 인형에 솜 넣는 동작
    public void PutCottonInDoll(GameObject doll, GameObject cotton)
    {
        int currentStateIndex = -1;
        for (int i = 0; i <= 2; i++)
        {
            GameObject currentState = doll.transform.Find("TestDoll_" + i).gameObject;
            if (currentState.activeSelf)
            {
                currentStateIndex = i; break;
            }
        }

        if (currentStateIndex != -1 && currentStateIndex < 2)
        {
            GameObject currentState = doll.transform.Find("TestDoll_" + currentStateIndex).gameObject;
            currentState.SetActive(false);
            GameObject nextState = doll.transform.Find("TestDoll_" + (currentStateIndex + 1)).gameObject;
            nextState.SetActive(true);

            Debug.Log("Changed to PreFab Status : " + nextState.name);
        }

        GameObject.Destroy(cotton); // 해당 솜 오브젝트 파괴
        Debug.Log($"Success to interaction between Doll and {cotton.name} ");
    }
}
