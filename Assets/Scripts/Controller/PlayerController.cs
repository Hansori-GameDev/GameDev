using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    // Private 변수인 speed를 inspector에 노출
    [SerializeField]
    float speed = 10f;

    void Start()
    {
        // 이미 액션이 할당되어 있을 경우 삭제 후 다시 추가
        Manager.Input.KeyAction -= ControllByWASD;
        Manager.Input.KeyAction += ControllByWASD;
    }

    // WASD 키를 통한 플레이어 컨트롤
    void ControllByWASD()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
