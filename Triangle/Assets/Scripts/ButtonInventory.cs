using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInventory : MonoBehaviour
{
    
    public PlayerItemInteraction player;
    public Inventory inventory;
    public CraftInventory craftinventory;

 
    public void UseItem()
    {
        inventory = player.GetInventory();

        Image image = transform.Find("image").GetComponent<Image>();
        Sprite sprite = image.sprite;

        foreach (Item item in inventory.GetItemList())
        {
            
            if (sprite == item.GetSprite())
            {
                
                if (item.IsCraftItem())
                {
                    //Craftfunktion
                    //item appears in craftsystem
                    //if crafting is commited new item into inventory, old disapears
                    Debug.Log("craft");
                    craftinventory.AddItem(item);
                    break;
                }

                if (item.IsEatItem())
                {
                    //Eatfunction
                    //diffrent sprites for meat
                    //Delet from Inventory
                    //Flash Player
                    //Change Element
                    Debug.Log("Eat");
                    break;
                }

                if (item.IsEquipItem())
                {
                    //Equip
                    //see if it is something equiped
                    //change colour of itembackround
                    //change stats
                    //place in players hand
                    Debug.Log("Equip");
                    break;
                }

                if (item.itemType == Item.ItemType.Crystal)
                {
                    //check amount: exit level if amount high enough
                    //or show how much to go
                    Debug.Log("Crystal");
                    break;
                }
            }
        }

    }




}
