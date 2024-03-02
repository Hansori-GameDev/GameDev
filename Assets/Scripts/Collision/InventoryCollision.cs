using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCollision : MonoBehaviour
{
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 만약 inventory에 순간이동해온 오브젝트가 Cotton일 경우 Log를 출력하고 Manager.Inventory에 해당 오브젝트 저장
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;
        if (item.CompareTag("Cotton"))
        {
            Debug.Log("Put Cotton in Inventory!");
            Manager.Inventory.putItem(item);
        }
    }
}
