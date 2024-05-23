using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour
{
    AnimStates.State _state;
    private float dir = 0;

    [SerializeField]
    private VirtualJoystick virtualJoystick;
    [SerializeField]
    private float movespeed = 10;
    private float animspeed = 1;
    Animator anim;

    private bool canMove = true; // 플레이어가 움직일 수 있는지 여부를 결정하는 변수

    void Start()
    {
        // 저장된 위치 데이터불러오기
        if (Manager.Data.getIsLoad())
        {
            transform.position = new Vector3(Manager.Data.nowPlayer.PlayerPosX, Manager.Data.nowPlayer.PlayerPosY, 0);
        }
        else
        {
            Debug.Log("Load fail");
        }
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float x = virtualJoystick.Horizontal(); // 왼쪽 / 오른쪽
            float y = virtualJoystick.Vertical();   // 아래 / 위

            // 키보드 입력을 통한 움직임 추가
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

            if (x != 0 || y != 0)
            {
                PlayerMove(new Vector2(x, y));
            }
            else
            {
                PlayerIdle();
            }
        }
    }

    void PlayerMove(Vector2 direction)
    {
        _state = AnimStates.State.Walk;
        transform.Translate(direction * movespeed * Time.fixedDeltaTime);
        animspeed = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        anim.SetFloat("animspeed", animspeed);

        dir = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        Manager.Animation.Play(anim, _state, dir);
    }

    void PlayerIdle()
    {
        _state = AnimStates.State.Idle;
        Manager.Animation.Play(anim, _state, dir);
    }

    // 플레이어가 움직일 수 있는지 여부를 설정하는 메소드
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
