using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyinInventory = false;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyinInventory = true;
                }

            }

            if (!itemAlreadyinInventory)
            {
                itemList.Add(item);
            }

        }
        else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public void DropItems(Vector3 position)
    {
        float x = 0f;

        foreach (Item item in itemList)
        {
            int amount = item.amount;
            foreach (int i in Enumerable.Range(0, amount))
            {
                Vector3 vector1 = new Vector3(x, 0);
                item.amount = 1;
                ItemWorld.SpawnItemWorld(position + vector1,item);
                x = x+ 2f;
            }
            
        }
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
