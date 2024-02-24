using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    // private 버튼
    Button uiButton;

    Transform inventoryPos;

    // 인벤토리 위치 Setter
    public void SetInventoryPos(Transform location)
    {
        inventoryPos = location;
    }

    // 버튼 Setter
    public void SetButton(Button button)
    {
        uiButton = button;
    }

    // 버튼 이미지 변경 메서드
    public void ChangeButtonImage(Sprite newImage)
    {
        uiButton.image.sprite = newImage;
    }

    // 디폴트 버튼
    public void DefaultButtonOnClick()
    {
        uiButton.onClick.RemoveAllListeners();
        uiButton.onClick.AddListener(() => OnButtonClickWithoutCollision());
    }

    // 상호작용 버튼 활성화
    public void interactionButtonOnClick(Collider2D collision)
    {
        uiButton.onClick.RemoveAllListeners();
        uiButton.onClick.AddListener(() => OnButtonClickDuringInteraction());
    }

    // 아이템 먹기 버튼 활성화
    public void PickItemButtonOnClick(Collider2D collision)
    {
        uiButton.onClick.RemoveAllListeners();
        uiButton.onClick.AddListener(() => OnButtonClickDuringPick(collision.gameObject));
    }

    // 충돌 중이 아닐 때 클릭 시 수행할 동작
    public void OnButtonClickWithoutCollision()
    {
        Debug.Log("충돌 중이 아닐 때 버튼이 클릭되었습니다.");
    }

    // 상호작용 버튼 클릭 시 수행할 동작
    public void OnButtonClickDuringInteraction()
    {

    }

    // 아이템 먹기 버튼 클릭 시 수행할 동작
    public void OnButtonClickDuringPick(GameObject collidedObject)
    {
        Debug.Log(collidedObject.name + " 충돌 중일 때 버튼이 클릭되었습니다.");

        // 충돌한 물체를 새로운 위치로 순간 이동
        collidedObject.transform.position = inventoryPos.position;
    }
}