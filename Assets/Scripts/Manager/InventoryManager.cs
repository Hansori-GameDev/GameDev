using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager
{
    GameObject holdingItem;

    public string GetHoldingItem()
    {
        if (holdingItem == null) return "";
        else return holdingItem.name;
    }

    public void SetHoldingItem(GameObject item)
    {
        this.holdingItem = item;
    }

    public void EmptyInventory()
    {
        if (holdingItem != null)
        {
            Object.Destroy(holdingItem);
            holdingItem = null;
        }
    }
}
