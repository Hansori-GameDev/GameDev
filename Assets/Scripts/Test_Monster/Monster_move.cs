using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_move : MonoBehaviour
{
    Monster_pathfinding Monster_pathfinding;
    [SerializeField] float speed = 0.025f; //increase for faster movement

    void Start()
    {
        Monster_pathfinding = GetComponent<Monster_pathfinding>();
    }

    void FixedUpdate()
    {
        if (Monster_pathfinding.pathLeftToGo.Count > 0) //if the target is not yet reached
        {
            Vector3 dir =  (Vector3)Monster_pathfinding.pathLeftToGo[0]-transform.position ;
            MonsterTurn((Vector3)Monster_pathfinding.pathLeftToGo[0]);
            transform.position += dir.normalized * speed;
            if (((Vector2)transform.position - Monster_pathfinding.pathLeftToGo[0]).sqrMagnitude <speed*speed) 
            {
                transform.position = Monster_pathfinding.pathLeftToGo[0];
                Monster_pathfinding.pathLeftToGo.RemoveAt(0);
            }

            for (int i=0;i<Monster_pathfinding.pathLeftToGo.Count-1;i++) //visualize your path in the sceneview
            {
                Debug.DrawLine(Monster_pathfinding.pathLeftToGo[i], Monster_pathfinding.pathLeftToGo[i+1]);
            }
        }
    }

    void MonsterTurn(Vector3 targetPosition) {
        Vector3 direction = (transform.position - targetPosition).normalized;

        float turnDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, turnDeg + 90);
    }
}