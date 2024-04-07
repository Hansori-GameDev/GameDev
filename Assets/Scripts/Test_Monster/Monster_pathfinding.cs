using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class Monster_pathfinding : MonoBehaviour
{
    [SerializeField] string enemyName;
    [SerializeField] bool DebugMode = true;
    [Range(0f, 360f)] [SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;

    List<Collider2D> hitTargetList = new List<Collider2D>();
    private GameObject player;

    [SerializeField] Transform waypoints;
    private int currentWaypointIndex = 0;

    float chaseTime;
    bool isRunningCoroutine = false;
    Vector3 targetPath;

    /******************
    * For Pathfinding *
    ******************/
    [Header("Navigator options")]
    [SerializeField] float gridSize = 0.9f; //increase patience or gridSize for larger maps
    
    Pathfinder<Vector2> pathfinder; //the pathfinder object that stores the methods and patience
    [Tooltip("The layers that the navigator can not pass through.")]
    [SerializeField] LayerMask obstacles;

    Vector2 targetNode; //target in 2D space
    List <Vector2> path;
    public List<Vector2> pathLeftToGo= new List<Vector2>();
    [SerializeField] bool drawDebugLines;
    /******************
    * For Pathfinding *
    ******************/


    void Start()
    {
        if (name.Equals("Monster1"))
        {
            InitStatus("Monster1", 65f, 5f);
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject;
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        // Pathfinding
        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes, 1000);
    }

    void FixedUpdate()
    {

        if(chaseTime > 0) {
            ChasePlayer();
        } else {
            MoveToWaypoint();
        }

        if(!isRunningCoroutine) {
            StartCoroutine("addPath");
        }
        // MonsterMove();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Game Over!");
        }
    }

    private void InitStatus(string _enemyName, float _ViewAngle, float _ViewRadius)
    {
        enemyName = _enemyName;
        ViewAngle = _ViewAngle;
        ViewRadius = _ViewRadius;
        TargetMask = LayerMask.GetMask("Default");
        ObstacleMask = LayerMask.GetMask("Ignore Raycast");
    }

    Vector2 AngleToDir(float angle) {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
    }

    void MonsterTurn(Vector3 targetPosition) {
        Vector3 direction = (transform.position - targetPosition).normalized;

        float turnDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, turnDeg + 90);
    }

    void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints.GetChild(currentWaypointIndex);

        targetPath = targetWaypoint.position;

        if (Vector2.Distance(transform.position, targetWaypoint.position) <= gridSize)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.childCount;
        }
    }

    void ChasePlayer()
    {
        targetPath = player.transform.position;
        chaseTime -= Time.fixedDeltaTime;
    }

    void FindTargets() {
        hitTargetList.Clear();

        Collider2D[] Targets = Physics2D.OverlapCircleAll(transform.position, ViewRadius, TargetMask);

        if (Targets.Length == 0) return;

        foreach(Collider2D col in Targets) {
            if(col.tag != "Player") {
                continue;
            }

            Vector3 targetPos = col.transform.position;
            Vector3 targetDir = new Vector3((targetPos - transform.position).x, (targetPos - transform.position).y, 0).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(transform.up, targetDir)) * Mathf.Rad2Deg;

            RaycastHit2D rayHitedTarget = Physics2D.Raycast(transform.position, targetDir, ViewRadius, TargetMask, -5, 10);

            if(targetAngle <= ViewAngle * 0.5)
            {
                if(rayHitedTarget && rayHitedTarget.collider.name == "Player") {
                    // Find Player
                    hitTargetList.Add(col);
                    chaseTime = 5f;
                    if (DebugMode) {
                        Debug.DrawLine(transform.position, targetPos, Color.red);
                    }
                } else {
                    if(DebugMode) {
                        Debug.DrawLine(transform.position, rayHitedTarget.point, Color.yellow);
                    }
                }        
            }   
        }
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

        FindTargets();
    }

    IEnumerator addPath() 
    {
        isRunningCoroutine = true;
        GetMoveCommand(new Vector2(targetPath.x, targetPath.y));
        yield return new WaitForSeconds(0.001f);
        isRunningCoroutine = false;
    }
    
    /******************
    * For Pathfinding *
    ******************/
    void GetMoveCommand(Vector2 target)
    {
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            pathLeftToGo = new List<Vector2>(path);
        }
    }

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
