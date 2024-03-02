using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    // 화면 우측 하단 상호작용 버튼
    Button uiButton;

    Button inventroyButton;

    // 인벤토리 위치
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

    public void SetInventoryButton(Button button)
    {
        inventroyButton = button;
        inventroyButton.onClick.RemoveAllListeners();
        inventroyButton.onClick.AddListener(() => Manager.Inventory.printInentroy());

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
        uiButton.onClick.AddListener(() => OnButtonClickDuringInteraction(collision.gameObject));
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
    public void OnButtonClickDuringInteraction(GameObject collidedObject)
    {
<<<<<<< Updated upstream
        Debug.Log(collidedObject.name + " 충돌 중일 때 버튼이 클릭되었습니다.");
=======
        Debug.Log($"Button is Clicked triggering with {collidedObject.name}");

        // 솜 보유시 인형에 채워넣음
        if (collidedObject.tag == "Doll")
        {
            // 인벤토리에서 솜 존재 여부 탐색
            GameObject item = Manager.Inventory.getCotton();

            // 만약 솜이 인벤토리 상에 있다면
            if (item)
            {
                Debug.Log("Try to put Cotton in Doll");
                Manager.Interaction.PutCottonInDoll(collidedObject, item);  // 솜을 인형에 넣음
            }
            // 만약 솜이 인벤토리 상에 없다면
            else
            {
                Debug.Log("There is no Cotton in your inventory!!");
            }
        }
>>>>>>> Stashed changes
    }

    // 아이템 먹기 버튼 클릭 시 수행할 동작
    public void OnButtonClickDuringPick(GameObject collidedObject)
    {
        Debug.Log($"Button is Clicked triggering with {collidedObject.name}");

<<<<<<< Updated upstream
        // 충돌한 물체를 새로운 위치로 순간 이동
=======
        // 인벤토리의 포화 상태 여부 체크	
        if (Manager.Inventory.getNum() >= Manager.Inventory.getMax())
        {
            Debug.Log("Inventory is Full!");
            return;
        }
        // 충돌한 물체를 새로운 위치로 순간 이동 -> 순간이동된 공간에서 아이템 종류 체크 후 인벤토리에 저장
>>>>>>> Stashed changes
        collidedObject.transform.position = inventoryPos.position;
    }
}