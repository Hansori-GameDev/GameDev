using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    float attackDelay;

    Monster monster;
    // Animator monsterAnimator;

    public Transform[] waypoints; // 몬스터가 따라갈 경로의 지점들
    private int currentWaypointIndex = 0; // 현재 몬스터가 이동 중인 지점의 인덱스

    void Start()
    {
        // 플레이어를 찾아서 할당
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        monster = GetComponent<Monster>();
        // monsterAnimator = monster.monsterAnimator;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, player.position);

        if (attackDelay == 0 && distance <= monster.fieldOfVision)
        {
            if (CanSeePlayer())
            {
                // 플레이어가 시야 내에 있을 때의 동작
                FacePlayer();

                if (distance <= monster.atkRange)
                {
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            }
            else
            {
                // 플레이어가 시야 밖에 있거나 벽이 있을 때의 동작
                MoveToWaypoint();
            }

        }
        else
        {
            // monsterAnimator.SetBool("moving", false);
        }
    }

    void ChasePlayer()
    {
        float dir = player.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * monster.moveSpeed * Time.deltaTime);
        // monsterAnimator.SetBool("moving", true);
    }

    void MoveToWaypoint()
    {
        // 현재 몬스터가 향하는 지점
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 몬스터를 지정된 지점 방향으로 이동
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, monster.moveSpeed * Time.deltaTime);

        // 만약 현재 지점에 도달했다면 다음 지점을 향하도록 설정
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // monsterAnimator.SetBool("moving", true);
    }

    void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;

        // X 축 기준으로 회전
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0) // 타겟이 왼쪽에 있을 때
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else // 타겟이 오른쪽에 있을 때
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        // Y 축 기준으로 회전
        else
        {
            if (direction.y < 0) // 타겟이 아래에 있을 때
            {
                // 아래를 보도록 회전
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else // 타겟이 위에 있을 때
            {
                // 위를 보도록 회전
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void AttackPlayer()
    {
        // player.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
        // monsterAnimator.SetTrigger("attack"); // 공격 애니메이션 실행
        attackDelay = monster.atkSpeed; // 딜레이 충전
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // 방향으로 레이캐스트를 쏴서 플레이어를 볼 수 있는지 확인
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // 플레이어와 몬스터 사이에 벽이 있음
            return false;
        }

        // 벽이 없으면 플레이어를 볼 수 있음
        return true;
    }
}
