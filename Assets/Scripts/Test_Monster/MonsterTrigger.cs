using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    public int count = 0;
    public bool isFullCount = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MonsterArea"))
        {
            count++; 
        }

        if (count >= 5)
        {
            isFullCount = true;
            Debug.Log(isFullCount); 
        }
    }

    
}