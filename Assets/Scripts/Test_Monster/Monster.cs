using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] string enemyName;
    [SerializeField] float moveSpeed;
    [SerializeField] float atkRange;
    public float fieldOfVision;

    public GameObject canvas;
    public float height = 1.7f;

    [SerializeField] bool DebugMode = false;
    [Range(0f, 360f)] [SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;

    public List<Collider2D> hitTargetList = new List<Collider2D>();

    private Transform player;

    [SerializeField] Transform[] waypoints;
    private int currentWaypointIndex = 0;

    public Animator monsterAnimator;

    void Start()
    {
        if (name.Equals("Monster1"))
        {
            SetEnemyStatus("Monster1", 1.5f, 1.5f, 7f);
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void SetEnemyStatus(string _enemyName, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    void Update()
    {
        /**
            위치찾아 이동 -> 플레이어 발견 -> 일정시간 따라가기 -> 플레이어가 범위를 벗어남 -> 반복
            coroutine : 따라가기
        **/
        float distance = Vector3.Distance(transform.position, player.position);

        if (FindPlayer())
        {
            // FacePlayer();

            if (distance <= atkRange)
            {
                // AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            MoveToWaypoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
            // Manager.Instance.GameOver(); 
        }
    }

    Vector2 AngleToDir(float angle) {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }

    void AttackPlayer()
    {
        // player.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
        // monsterAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
        
    }

    void MonsterTurn(Vector3 targetPosition) {
        Vector3 direction = (transform.position - targetPosition).normalized;

        float turnDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, turnDeg + 90);
    }

    void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        MonsterTurn(targetWaypoint.position);
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.3f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // monsterAnimator.SetBool("moving", true);
    }

    bool FindPlayer()
    {
        if (hitTargetList.Count != 0)
        {
            return true;
        }

        return false;
    }

    void ChasePlayer()
    {
        Vector3 dir = player.position - transform.position;
        MonsterTurn(player.position);
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnDrawGizmos() {
        if (!DebugMode) return;

        Vector2 myPos = (Vector2)transform.position + Vector2.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);

        float lookingAngle = -transform.eulerAngles.z;  // 캐릭터가 바라보는 방향의 각도
        Vector2 rightDir = AngleToDir(lookingAngle + ViewAngle * 0.5f);
        Vector2 leftDir = AngleToDir(lookingAngle - ViewAngle * 0.5f);
        Vector2 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(transform.position, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(transform.position, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(transform.position, lookDir * ViewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider2D[] Targets = Physics2D.OverlapCircleAll(transform.position, ViewRadius, TargetMask);

        if (Targets.Length == 0) return;

        foreach(Collider2D col in Targets) {
            Vector3 targetPos = col.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(transform.up, targetDir)) * Mathf.Rad2Deg;
            if(targetAngle <= ViewAngle * 0.5f && !Physics2D.Raycast(transform.position, targetDir, ViewRadius, 0, ViewRadius))
            {
                hitTargetList.Add(col);
                if (DebugMode) Debug.DrawLine(transform.position, targetPos, Color.red);
                /** 플레이어 따라가는 함수 **/
            }
        }
    }
}
