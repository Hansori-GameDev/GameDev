using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    // private 버튼
    Button uiButton;

    // 버튼 이미지 변경 메서드
    public void ChangeButtonImage(Sprite newImage)
    {
        uiButton.image.sprite = newImage;
    }

    // 버튼 Setter
    public void SetButton(Button button)
    {
        uiButton = button;
    }
}