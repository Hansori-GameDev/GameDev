using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpZone : MonoBehaviour
{
    public GameObject obj;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            Debug.Log(col);
            Manager.Interaction.WarpPlayerToOtherFloor(col.gameObject, obj.transform.position);
            
        }
    }
}
