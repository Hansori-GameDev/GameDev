using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    // 화면 우측 하단 상호작용 버튼
    Button uiButton;

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
        Debug.Log(collidedObject.name + " 충돌 중일 때 버튼이 클릭되었습니다.");

        // 솜 보유시 인형에 채워넣음
        if (collidedObject.tag == "Doll")
        {
            if (Manager.Inventory.GetHoldingItem() == "Cotton")
            {
                Debug.Log("인형에 솜 넣음");
                PutCottonInDoll(collidedObject);
            }
            else
            {
                Debug.Log("들고 있는 아이템이 솜이 아닙니다.");
            }
        }
    }

    // 인형에 솜 넣는 동작
    private void PutCottonInDoll(GameObject collidedObject)
    {
        int currentStateIndex = -1;
        for (int i = 0; i <= 2; i++)
        {
            GameObject currentState = collidedObject.transform.Find("TestDoll_" + i).gameObject;
            if(currentState.activeSelf)
            {
                currentStateIndex = i; break;
            }

            Debug.Log("현재 PreFab: " + currentState.name);
        }

        if (currentStateIndex != -1 && currentStateIndex < 2) 
        {
            GameObject currentState = collidedObject.transform.Find("TestDoll_" + currentStateIndex).gameObject;
            currentState.SetActive(false);
            GameObject nextState = collidedObject.transform.Find("TestDoll_" + (currentStateIndex + 1)).gameObject;
            nextState.SetActive(true);
        }

        Manager.Inventory.EmptyInventory();
        Debug.Log("솜 넣는 처리 완료");
    }

    // 아이템 먹기 버튼 클릭 시 수행할 동작
    public void OnButtonClickDuringPick(GameObject collidedObject)
    {
        Debug.Log(collidedObject.name + " 충돌 중일 때 버튼이 클릭되었습니다.");

        // 충돌한 물체를 새로운 위치로 순간 이동
        if (Manager.Inventory.GetHoldingItem() != "")
        {
            Debug.Log("하나의 아이템만 소유할 수 있습니다.");
        }
        else collidedObject.transform.position = inventoryPos.position;
    }
}