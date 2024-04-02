using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class Monster_pathfinding : MonoBehaviour
{
    [SerializeField] string enemyName;
    [SerializeField] float moveSpeed;
    [SerializeField] float atkRange;

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

    public float chaseTime;

    /******************
    * For Pathfinding *
    ******************/
    [Header("Navigator options")]
    [SerializeField] float gridSize = 0.5f; //increase patience or gridSize for larger maps
    [SerializeField] float speed = 0.05f; //increase for faster movement
    
    Pathfinder<Vector2> pathfinder; //the pathfinder object that stores the methods and patience
    [Tooltip("The layers that the navigator can not pass through.")]
    [SerializeField] LayerMask obstacles;
    [Tooltip("Deactivate to make the navigator move along the grid only, except at the end when it reaches to the target point. This shortens the path but costs extra Physics2D.LineCast")] 
    [SerializeField] bool searchShortcut =false; 
    [Tooltip("Deactivate to make the navigator to stop at the nearest point on the grid.")]
    [SerializeField] bool snapToGrid =false; 
    Vector2 targetNode; //target in 2D space
    List <Vector2> path;
    List<Vector2> pathLeftToGo= new List<Vector2>();
    [SerializeField] bool drawDebugLines;


    void Start()
    {
        if (name.Equals("Monster1"))
        {
            SetEnemyStatus("Monster1", 1.5f, 1.5f);
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

        // Pathfinding
        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000);
    }

    private void SetEnemyStatus(string _enemyName, float _moveSpeed, float _atkRange)
    {
        enemyName = _enemyName;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
    }

    void Update()
    {
        /**
            위치찾아 이동 -> 플레이어 발견 -> 일정시간 따라가기 -> 플레이어가 범위를 벗어남 -> 반복
            coroutine : 따라가기
            변수 하나를 정해서 따라가는 시간으로 설정
        **/
        float distance = Vector3.Distance(transform.position, player.position);

        FindPlayer();

        if(chaseTime > 0) {
            ChasePlayer();
        } else {
            MoveToWaypoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 게임종료
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

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void FindPlayer()
    {
        if (hitTargetList.Count != 0)
        {
            chaseTime = 5f;
        }
    }

    void ChasePlayer()
    {
        Debug.Log(player.position);
        if(chaseTime >= 0f) {
            Debug.Log(Camera.main.ScreenToWorldPoint(player.position));
            // Vector3 dir = player.position - transform.position;
            // MonsterTurn(player.position);
            // transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
            // chaseTime -= Time.deltaTime;
        }
    }

    void FindTargets() {

        hitTargetList.Clear();
        Collider2D[] Targets = Physics2D.OverlapCircleAll(transform.position, ViewRadius, TargetMask);
       
        Debug.Log(Targets.Length);

        if (Targets.Length == 0) return;

        foreach(Collider2D col in Targets) {
            if(col.name == "wall") {
                Debug.Log("wall");
                continue;
            }

            Vector3 targetPos = col.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;

            RaycastHit2D rayHitedTarget = Physics2D.Raycast(transform.position, targetDir, ViewRadius, ObstacleMask);

            if(rayHitedTarget) {
                Debug.DrawLine(transform.position, rayHitedTarget.point, Color.yellow);
            } else {
                
                float targetAngle = Mathf.Acos(Vector3.Dot(transform.up, targetDir)) * Mathf.Rad2Deg;
                if(targetAngle <= ViewAngle * 0.5f && !Physics2D.Raycast(transform.position, targetDir, ViewRadius, 0, ViewRadius))
                {
                    hitTargetList.Add(col);
                    if (DebugMode) {
                        Debug.DrawLine(transform.position, targetPos, Color.red);
                    }
                    /** 플레이어 따라가는 함수 **/
                }
            }           
        }

        // hitTargetList.Clear();
        // Collider2D[] Targets = Physics2D.OverlapCircleAll(transform.position, ViewRadius, ObstacleMask);

        // if (Targets.Length == 0) return;

        // foreach(Collider2D col in Targets) {
        //     if(col.name == "wall") {
        //         continue;
        //     }
        //     Vector3 targetPos = col.transform.position;
        //     Vector3 targetDir = (targetPos - transform.position).normalized;
        //     float targetAngle = Mathf.Acos(Vector3.Dot(transform.up, targetDir)) * Mathf.Rad2Deg;
        //     if(targetAngle <= ViewAngle * 0.5f && !Physics2D.Raycast(transform.position, targetDir, ViewRadius, 0, ViewRadius))
        //     {
        //         hitTargetList.Add(col);
        //         if (DebugMode) {
        //             Debug.DrawLine(transform.position, targetPos, Color.red);
        //         }
        //         /** 플레이어 따라가는 함수 **/
        //     }
        // }
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
            if(col.tag == "Wall") {
                Debug.Log("wall return");
                continue;
            }

            Vector3 targetPos = col.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(transform.up, targetDir)) * Mathf.Rad2Deg;

            RaycastHit2D rayHitedTarget = Physics2D.Raycast(transform.position, targetDir, ViewRadius, TargetMask, 0, ViewRadius);

            Debug.Log(rayHitedTarget.point);

            if(rayHitedTarget && rayHitedTarget.collider.name == "Player") {
                if(targetAngle <= ViewAngle * 0.5)
                {
                    hitTargetList.Add(col);
                    if (DebugMode) {
                        Debug.DrawLine(transform.position, targetPos, Color.red);
                    }
                }
            } else {
                Debug.DrawLine(transform.position, rayHitedTarget.point, Color.yellow);
            }           
        }
    }


    
    void GetMoveCommand(Vector2 target)
    {
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            if (searchShortcut && path.Count>0)
                pathLeftToGo = ShortenPath(path);
            else
            {
                pathLeftToGo = new List<Vector2>(path);
                if (!snapToGrid) pathLeftToGo.Add(target);
            }

        }
        
    }


    /******************
    * For Pathfinding *
    ******************/

    /// <summary>
    /// Finds closest point on the grid
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    Vector2 GetClosestNode(Vector2 target) 
    {
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    /// <summary>
    /// A distance approximation. 
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    float GetDistance(Vector2 A, Vector2 B) 
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    /// <summary>
    /// Finds possible conenctions and the distances to those connections on the grid.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos) 
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;
                if (!Physics2D.Linecast(pos,pos+dir, obstacles))
                {
                    neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    
    List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();
        
        for (int i=0;i<path.Count;i++)
        {
            newPath.Add(path[i]);
            for (int j=path.Count-1;j>i;j-- )
            {
                if (!Physics2D.Linecast(path[i],path[j], obstacles))
                {
                    
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }
}
