using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftInventory : MonoBehaviour
{
    private List<Item> craftList;
    public UiCraft uiCraft;

    public CraftInventory()
    {
        craftList = new List<Item>();
    }

    public List<Item> GetCraftList()
    {
        return craftList;
    }

    public void AddItem(Item item)
    {
        switch (craftList.Count)
        {
            default:

            case 0: 
                craftList.Add(item);
                uiCraft.ShowCraftSlotItem(craftList);
                break;

            case 1:
                craftList.Add(item);
                uiCraft.ShowCraftSlotItem(craftList);
                Item resultItem = GetResult();
                if (resultItem.itemType != Item.ItemType.None)
                {
                    uiCraft.ShowResultItem(resultItem);

                }
                
                break;
            case 2:
                Debug.Log("Full");
                break;
        }

    }

    private Item GetResult()
    {
        
        var sortedList = craftList.OrderBy(c => c.itemType).ToList();
        

        if (sortedList[0].itemType == Item.ItemType.Wood && sortedList[1].itemType == Item.ItemType.Iron)
        {
            return new Item { itemType = Item.ItemType.Sword, amount = 1 };  
        }
        if (sortedList[0].itemType == Item.ItemType.Wood && sortedList[1].itemType == Item.ItemType.Fur)
        {
            return new Item { itemType = Item.ItemType.Bow, amount = 1 };
        }
        if (sortedList[0].itemType == Item.ItemType.Iron && sortedList[1].itemType == Item.ItemType.Fur)
        {
            return new Item { itemType = Item.ItemType.Armour, amount = 1 };
        }

        return new Item { itemType = Item.ItemType.None, amount = 1 };

    }

    public void RemoveItem(int i)
    {
        craftList.RemoveAt(i);
    }

    public void HideSlots()
    {
        uiCraft.HideItemSlot1();
        uiCraft.HideItemSlot2();
        uiCraft.HideResultImage();
    }
}
