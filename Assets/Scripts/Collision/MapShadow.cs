using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShadow : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas1F;
    [SerializeField]
    private GameObject Canvas2F;
    [SerializeField]
    private GameObject Player;

    private GameObject disabled_object;

    private void FixedUpdate() {
        this.transform.position = Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Map") {
            if(col.gameObject.name == "Stairs") {
                return;
            } else {
                if(disabled_object) {
                    disabled_object.SetActive(true);
                }

                disabled_object = Canvas1F.transform.Find(col.gameObject.name).gameObject;
                Canvas1F.transform.Find(col.gameObject.name).gameObject.SetActive(false);
            }
        }
    }
}
