using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour
{
    [SerializeField]
    private VirtualJoystick virtualJoystick;
    private float movespeed = 10;
    public Transform player;


    private void Update()
    {
        float x = virtualJoystick.Horizontal(); // 왼 / 오
        float y = virtualJoystick.Vertical();   // 위 / 아래
 
        if (x!=0 || y!=0)
        {
            transform.position += new Vector3(x, y, 0) * movespeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Item")
        {
            Debug.Log("상호작용");

            GameObject actionKey = GameObject.FindWithTag("ActionKey");
            Interaction actionKeyInteraction = actionKey.GetComponent<Interaction>();

            if (actionKeyInteraction != null)
            {
                // Interaction의 isInteractable 값을 true로 설정
                actionKeyInteraction.isInteractable = true;
            }

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("상호작용 종료");

            GameObject actionKey = GameObject.FindWithTag("ActionKey");
            Interaction actionKeyInteraction = actionKey.GetComponent<Interaction>();

            if (actionKeyInteraction != null)
            {
                // Interaction의 isInteractable 값을 false로 설정
                actionKeyInteraction.isInteractable = false;
            }
        }
        }


}
