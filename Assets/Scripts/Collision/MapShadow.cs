using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShadow : MonoBehaviour
{
    public Timer testTimer;
    [SerializeField]
    private GameObject MapCanvas;
    [SerializeField]
    private GameObject Player;

    private GameObject disabled_object = null;

    private void start() {
        testTimer = GameObject.Find("EventSystem").GetComponent<Timer>();
    }

    private void FixedUpdate() {
        this.transform.position = Player.transform.position;
    }

        /**
            TODO Coroutine으로 구현, 딜레이 추가
        **/
    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Map") {
            if(col.gameObject.name == "Stairs" || (disabled_object && disabled_object.name == col.gameObject.name)) {
                return;
            } else {
                if(disabled_object) {
                    disabled_object.SetActive(true);
                }

                disabled_object = MapCanvas.transform.Find(col.gameObject.name).gameObject;
                MapCanvas.transform.Find(col.gameObject.name).gameObject.SetActive(false);
            }
        }

        if(col.gameObject.name == "Timer") {
            testTimer.startTimer();
        }
    }
}
