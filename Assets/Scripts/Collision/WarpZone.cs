using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarpZone : MonoBehaviour
{
    public GameObject obj;
    public TextMeshProUGUI floorText;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            Debug.Log(col);
            int targetFloor = Manager.Interaction.GetCurrentFloor(col.gameObject);
            StartCoroutine(ShowFloorText(targetFloor));
            Manager.Interaction.WarpPlayerToOtherFloor(col.gameObject, obj.transform.position);
            
        }

        IEnumerator ShowFloorText(int floor)
        {
            floorText.text = floor + "F";
            floorText.gameObject.SetActive(true);

            // 서서히 나타내기
            float elapsedTime = 0f;
            Color textColor = floorText.color;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / 1f); // 투명도 서서히 증가
                floorText.color = textColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f); // 층수 표시가 완료 -> 1초 기다림 -> 다시 서서히 사라짐

            // 서서히 사라지기
            elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / 1f); // 투명도 서서히 감소
                floorText.color = textColor;
                yield return null;
            }

            floorText.gameObject.SetActive(false);
        }
    }
}
