using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string enemyName;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;

    public GameObject canvas;
    public float height = 1.7f;

    public Animator monsterAnimator;

    void Start()
    {
        if (name.Equals("Monster1"))
        {
            SetEnemyStatus("Monster1", 1.5f, 2, 1.5f, 7f);
        }

        SetAttackSpeed(atkSpeed);
    }

    private void SetEnemyStatus(string _enemyName, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;

        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    void Update()
    {
        // 이동 및 공격 관련 로직은 MonsterAI 클래스에서 처리
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 플레이어와 충돌 시 게임 오버
            // Manager.Instance.GameOver(); 
        }
    }

    void SetAttackSpeed(float speed)
    {
        // monsterAnimator.SetFloat("attackSpeed", speed);
    }
}
