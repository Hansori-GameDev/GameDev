using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    float attackDelay;

    Monster monster;
    // Animator monsterAnimator;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0; // ���� ���Ͱ� �̵� ���� ������ �ε���

    List<Collider> hitTargetList = new List<Collider>();

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

        FindPlayer();

        if (FindPlayer())
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

    void ChasePlayer()
    {
        Vector3 dir = player.position - transform.position;
        transform.Translate(dir.normalized * monster.moveSpeed * Time.deltaTime);
        // monsterAnimator.SetBool("moving", true);
    }

    void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetDirection = (transform.position - targetWaypoint.position).normalized;
        float turn_z = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, turn_z - 90);
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, monster.moveSpeed * Time.deltaTime);
        
        Debug.Log(targetDirection);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.3f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // monsterAnimator.SetBool("moving", true);
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position);

        // // X �� �������� ȸ��
        // if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        // {
        //     if (direction.x < 0) // Ÿ���� ���ʿ� ���� ��
        //     {
        //         transform.localScale = new Vector3(-1, 1, 1);
        //     }
        //     else // Ÿ���� �����ʿ� ���� ��
        //     {
        //         transform.localScale = new Vector3(1, 1, 1);
        //     }
        // }
        // // Y �� �������� ȸ��
        // else
        // {
        //     if (direction.y < 0) // Ÿ���� �Ʒ��� ���� ��
        //     {
        //         // �Ʒ��� ������ ȸ��
        //         transform.rotation = Quaternion.Euler(0, 0, 180);
        //     }
        //     else // Ÿ���� ���� ���� ��
        //     {
        //         // ���� ������ ȸ��
        //         transform.rotation = Quaternion.Euler(0, 0, 0);
        //     }
        // }

        transform.rotation = Quaternion.Euler(0, 0, direction.z * 360f);
    }

    void AttackPlayer()
    {
        // player.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
        // monsterAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
        attackDelay = monster.atkSpeed; // ������ ����
    }

    

    bool FindPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.5f);

        Debug.DrawRay(transform.position, transform.up, Color.red);

        if (monster.hitTargetList.Count != 0)
        {
            return true;
        }

        return false;
    }

}
