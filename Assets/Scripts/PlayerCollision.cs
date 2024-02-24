using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rigid;

    public Button button;
    public Sprite defaultSprite;
    public Sprite changedSprite_1;
    public Sprite changedSprite_2;

    private int isColliding = 0;

    public Transform inventoryPos; // 이동할 새로운 위치

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Manager.UI.SetButton(button);   // 매니저-버튼 Setter
        Manager.UI.SetInventoryPos(inventoryPos);   // 매니저-인벤토리 위치 Setter
        Manager.UI.DefaultButtonOnClick();  // 디폴트 버튼으로 초기 설정  
    }

    void Update()
    {
        // 충돌 중인 오브젝트가 없을 경우 디폴트 버튼 활성화
        if (isColliding == 0)
            Manager.UI.DefaultButtonOnClick();
    }

    // 물체와 충돌 발생 시 버튼 변경
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding++; // Flag값 증	

        if (collision.gameObject.tag == "Doll") // 충돌한 오브젝트가 Doll일 경우
        {
            Manager.UI.interactionButtonOnClick(collision); // 상호작용 버튼 활성화
            Manager.UI.ChangeButtonImage(changedSprite_1);
            Debug.Log(collision.gameObject.name + " 충돌 시작!");
        }

        if (collision.gameObject.tag == "Item") // 충돌한 오브젝트가 Item일 경우  
        {
            Manager.UI.PickItemButtonOnClick(collision);  // 아이템 먹기 버튼 활성화
            Manager.UI.ChangeButtonImage(changedSprite_2);
            Debug.Log(collision.gameObject.name + " 충돌 시작!");
        }
    }

    // 물체와 충돌 중일 경우
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " 충돌 중!");
    }

    // 물체와 충돌 종료 시
    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding--;  // Flag값 감

        Manager.UI.DefaultButtonOnClick();

        if (collision.gameObject.tag == "Doll")
        {
            Manager.UI.ChangeButtonImage(defaultSprite);
            Debug.Log(collision.gameObject.name + " 충돌 종료!");
        }
    }
}
