using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    float attackDelay;

    Monster monster;
    // Animator monsterAnimator;

    public Transform[] waypoints; // ���Ͱ� ���� ����� ������
    private int currentWaypointIndex = 0; // ���� ���Ͱ� �̵� ���� ������ �ε���

    void Start()
    {
        // �÷��̾ ã�Ƽ� �Ҵ�
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
                // �÷��̾ �þ� ���� ���� ���� ����
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
                // �÷��̾ �þ� �ۿ� �ְų� ���� ���� ���� ����
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
        // ���� ���Ͱ� ���ϴ� ����
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // ���͸� ������ ���� �������� �̵�
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, monster.moveSpeed * Time.deltaTime);

        // ���� ���� ������ �����ߴٸ� ���� ������ ���ϵ��� ����
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // monsterAnimator.SetBool("moving", true);
    }

    void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;

        // X �� �������� ȸ��
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0) // Ÿ���� ���ʿ� ���� ��
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else // Ÿ���� �����ʿ� ���� ��
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        // Y �� �������� ȸ��
        else
        {
            if (direction.y < 0) // Ÿ���� �Ʒ��� ���� ��
            {
                // �Ʒ��� ������ ȸ��
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else // Ÿ���� ���� ���� ��
            {
                // ���� ������ ȸ��
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void AttackPlayer()
    {
        // player.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
        // monsterAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
        attackDelay = monster.atkSpeed; // ������ ����
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // �������� ����ĳ��Ʈ�� ���� �÷��̾ �� �� �ִ��� Ȯ��
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // �÷��̾�� ���� ���̿� ���� ����
            return false;
        }

        // ���� ������ �÷��̾ �� �� ����
        return true;
    }
}
