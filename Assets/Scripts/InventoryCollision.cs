using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCollision : MonoBehaviour
{

    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;
        if (!item.CompareTag("Item"))
        {
            Debug.Log("�κ��丮�� Item�� �ƴ� ��ü�� �ֽ��ϴ�.");
        }
        Debug.Log("�κ��丮�� " + item.name + " ����");
        Manager.Inventory.SetHoldingItem(item);
    }
}
