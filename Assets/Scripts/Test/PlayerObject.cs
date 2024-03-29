using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public float _speed = 5f;
    public Vector2 _moveDirection; // UI에서 방향 입력받기
    private Rigidbody2D _rigid;

    void Start()
    {
        _rigid = this.GetComponent<Rigidbody2D>();

        // 저장된데이터 불러오기
        if(Manager.Data.getIsLoad()) {
            transform.position = new Vector3(Manager.Data.nowPlayer.PlayerPosX, Manager.Data.nowPlayer.PlayerPosY, 0);
        } else {
            Debug.Log("Load fail");
        }
    }

    void FixedUpdate()
    {
        _moveDirection = new Vector2(0, 0);

    /************
    * TEST CODE *
    ************/
        if (Input.GetKey(KeyCode.W))
            _moveDirection.y = 1f;
        if (Input.GetKey(KeyCode.S))
            _moveDirection.y = -1f;
        if (Input.GetKey(KeyCode.A))
            _moveDirection.x = -1f;
        if (Input.GetKey(KeyCode.D))
            _moveDirection.x = 1f;

        PlayerMove(_moveDirection);
    }

    void PlayerMove(Vector2 direction) {
        transform.Translate(direction.normalized * _speed * Time.fixedDeltaTime, Space.World);
        if(direction.x != 0 || direction.y != 0)
            PlayerRotate(direction);
    }

    void PlayerRotate(Vector2 direction) {
        float rotateAngle = -direction.x * 90;
        if(direction.y < 0) {
            rotateAngle = 180f;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
    }
}
