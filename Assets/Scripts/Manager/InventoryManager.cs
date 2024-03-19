using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item
{
    public string itemName;
    public string itemTag;
    public GameObject item;

    public Item()
    {
    }

    // 생성자
    public Item(string itemName, string itemTag, GameObject item)
    {
        this.itemName = itemName;
        this.itemTag = itemTag;
        this.item = item;
    }
}

// 인벤토리 클래스
class Inventory
{
    public int num = 0; // 현재 인벤토리 아이템 	
    public int size = 1;    // 인벤토리 최대 크기
    public Item[] itemSlot;   // 인벤토리 아이템 슬롯

    public Inventory()
    {
        itemSlot = new Item[size];    // size의 크기로 인벤토리 슬롯 생	
    }
}

public class InventoryManager
{
    Inventory inventory;    // 인벤토리 객체

    public InventoryManager()
    {
        inventory = new Inventory();    // 인벤토리 객체 생성
    }

    // 아이템을 인벤토리에 저장
    public void putItem(GameObject item)
    {
        Item itemInfo = new Item(item.name, item.tag, item);
        inventory.itemSlot[inventory.num] = itemInfo;
        inventory.num++;
    }

    // 인덱스로 조회한 아이템 pop
    public Item popItem(int index = 0)
    {
        Item item;

        if (index < 0 || index >= inventory.size)
        {
            Debug.Log("Invalid index!");
            return null;
        }

        if (inventory.itemSlot[index] == null)
        {
            Debug.Log("No item at index " + index);
            return null;
        }

        item = inventory.itemSlot[index];

        for (int i = index; i < inventory.num - 1; i++)
        {
            inventory.itemSlot[i] = inventory.itemSlot[i + 1];
        }

        inventory.itemSlot[inventory.num - 1] = null;
        inventory.num--;

        return item;
    }

    // 인벤토리에서 솜을 탐색하고 발견 시, 해당 오브젝트를 pop하여 반환
    public Item getCotton()
    {
        int targetIndex;

        for (targetIndex = inventory.num - 1; targetIndex >= 0; targetIndex--)
        {
            if (inventory.itemSlot[targetIndex].itemTag == "Cotton")
            {
                return popItem(targetIndex);
            }
        }

        return null;

    }

    // 현재 인벤토리에 저장된 아이템 수 반환
    public int getNum()
    {
        return inventory.num;
    }

    // 인벤토리의 최대 크기 반환
    public int getMax()
    {
        return inventory.size;
    }


    // 인벤토리 목록 출력
    public void printInentroy()
    {
        if (inventory.num != 0)
        {
            for (int i = 0; i < inventory.num; i++)
                Debug.Log($"Name: {inventory.itemSlot[i].itemName} / Tag: {inventory.itemSlot[i].itemTag}");
        }
        else
            Debug.Log("Inventory is Empty!");

    }
}