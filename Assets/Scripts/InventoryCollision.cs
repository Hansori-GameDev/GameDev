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
            Debug.Log("인벤토리에 Item이 아닌 물체가 있습니다.");
        }
        Debug.Log("인벤토리에 " + item.name + " 들어옴");
        Manager.Inventory.SetHoldingItem(item);
    }
}
