using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDied : MonoBehaviour
{
    public TMP_Text deathText;      // 사망 메세지
    public Image blackPanel;        // 검은색 패널
    public float fadeDuration = 2f; // 페이드 동작 시간
    public Timer timer;

    bool isPlayerDead = false;      // 사망 여부 플래그 변수 
    MovementObject movementObject;

    void Start()
    {
        blackPanel.gameObject.SetActive(false);

        // 시작 시에는 사망 텍스트와 검은색 패널을 숨김
        deathText.color = Color.clear;
        blackPanel.color = Color.clear;
        
        // MovementObject 컴포넌트 사용
        movementObject = GetComponent<MovementObject>();
    }

    void Update()
    {
        // 플레이어가 죽었을 때의 사망 시그널을 받아 처리 + 테스트를 위해 스페이스바를 누르면 사망하도록 임시로 구현
        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerDead || timer._isGameOver)
        {
            PlayerDead();
        }
    }

    // 플레이어 죽었을 때의 동작 함수
    void PlayerDead()
    {
        isPlayerDead = true;            // 사망 여부 플래그 갱신
        deathText.text = "Game Over";   // 사망 메세지 텍스트
        deathText.color = Color.red;    // 사망 메세지 색상
        blackPanel.gameObject.SetActive(true);
        StartCoroutine(FadeDeathText());
        
        // 플레이어를 움직이지 못하도록 MovementObject의 메소드를 호출하여 제한
        movementObject.SetCanMove(false);
    }

    IEnumerator FadeDeathText()
    {
        // 페이드 애니메이션
        float elapsedTime = 0f;
        Color initialColor = deathText.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // 0부터 1까지 점점 증가
            deathText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            blackPanel.color = new Color(0f, 0f, 0f, alpha * 0.8f); // 검은색 패널의 알파값 설정
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 페이드 동작이 끝나면 다른 동작 수행
        yield return new WaitForSeconds(1f); // 다음 동작을 위해 잠시 대기

        // 다음 동작 구현부

        //
    }
}
