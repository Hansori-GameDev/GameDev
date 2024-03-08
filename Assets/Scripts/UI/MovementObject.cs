using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour
{
    [SerializeField]
    private VirtualJoystick virtualJoystick;
    private float movespeed = 10;

    private void Start() {
        // 저장된 위치 데이터불러오기
        if(Manager.Data.getIsLoad()) {
            transform.position = new Vector3(Manager.Data.nowPlayer.PlayerPosX, Manager.Data.nowPlayer.PlayerPosY, 0);
        } else {
            Debug.Log("Load fail");
        }
    }

    private void FixedUpdate()
    {
        float x = virtualJoystick.Horizontal(); // �� / ��
        float y = virtualJoystick.Vertical();   // �� / �Ʒ�
        
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.W))
            y = 1f;
        if (Input.GetKey(KeyCode.S))
            y = -1f;
        if (Input.GetKey(KeyCode.A))
            x = -1f;
        if (Input.GetKey(KeyCode.D))
            x = 1f;
#endif
 
        if (x!=0 || y!=0) {
            PlayerMove(new Vector2(x, y));
        }
    }

    void PlayerMove(Vector2 direction) {
        transform.Translate(direction * movespeed * Time.fixedDeltaTime);
    }

}
