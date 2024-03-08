using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image imageBackground;
    private Image imageController;
    private Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();  
        imageController = transform.GetChild(0).GetComponent<Image>();
    }

    // 터치 시작 시 1회
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("Touch Begin : " + eventData);
    }

    // 터치 상태일 때 매 프레임
    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        // 조이스틱의 위치가 어디에 있든 동일한 값을 연산하기 위해
        // touchPosition의 위치 값은 이미지의 현재 위치를 기준으로
        // 얼마나 떨어져 있는지에 따라 다르게 나온다

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            // touchPosition 값의 정규화 [0 ~ 1]
            // touchPosition을 이미지 크기로 나눔
            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2 -1, touchPosition.y * 2 -1);
            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            imageController.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * imageBackground.rectTransform.sizeDelta.x / 2,
                touchPosition.y * imageBackground.rectTransform.sizeDelta.y / 2);

            //Debug.Log("Touch & Drag : " + eventData);
        }
    }

    // 터치 종료 시 1회
    public void OnPointerUp(PointerEventData eventData)
    {
        // 터치 종료 시 이미지의 위치를 다시 중앙으로 옮긴다
        imageController.rectTransform.anchoredPosition = Vector2.zero;

        // 다른 오브젝트에서 이동 방향으로 사용하기 때문에 이동 방향도 초기화
        touchPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }

    public float Vertical()
    {
        return touchPosition.y;
    }
}
