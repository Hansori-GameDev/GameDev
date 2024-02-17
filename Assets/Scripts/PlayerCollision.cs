using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rigid;

    public Button button;
    public Sprite defaultSprite;
    public Sprite changedSprite;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        Manager.UI.SetButton(button);
    }

    void Update()
    {

    }

    // Doll과 충돌 발생 시 버튼 모양 변경
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            Debug.Log("충돌 시작!");
            Manager.UI.ChangeButtonImage(changedSprite);
        }
    }

    // Doll과 충돌 중일 경우, 변경된 버튼 모양 유지
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            Debug.Log("충돌 중!");
        }
    }

    // Doll과 충돌 종료 시, 버튼 모양 복구
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            Debug.Log("충돌 종료!");
            Manager.UI.ChangeButtonImage(defaultSprite);
        }
    }
}
