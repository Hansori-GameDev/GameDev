using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        Application.targetFrameRate = 60;
        _player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
    }
}
