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
        float x = virtualJoystick.Horizontal(); // �� / ��
        float y = virtualJoystick.Vertical();   // �� / �Ʒ�
 
        if (x!=0 || y!=0)
        {
            transform.position += new Vector3(x, y, 0) * movespeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Item")
        {
            Debug.Log("��ȣ�ۿ�");

            GameObject actionKey = GameObject.FindWithTag("ActionKey");
            Interaction actionKeyInteraction = actionKey.GetComponent<Interaction>();

            if (actionKeyInteraction != null)
            {
                // Interaction�� isInteractable ���� true�� ����
                actionKeyInteraction.isInteractable = true;
            }

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Debug.Log("��ȣ�ۿ� ����");

            GameObject actionKey = GameObject.FindWithTag("ActionKey");
            Interaction actionKeyInteraction = actionKey.GetComponent<Interaction>();

            if (actionKeyInteraction != null)
            {
                // Interaction�� isInteractable ���� false�� ����
                actionKeyInteraction.isInteractable = false;
            }
        }
        }


}
