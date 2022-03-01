using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInventory : MonoBehaviour
{
    
    public PlayerItemInteraction playeritem;
    public PlayerCombat playercombat;
    
    public CraftInventory craftinventory;
    public EquipInventory equipInventory;

    private Inventory inventory;


    public void UseItem()
    {
        inventory = playeritem.GetInventory();

        //Get itemType of clicked Image
        Image image = transform.Find("image").GetComponent<Image>();
        Sprite sprite = image.sprite;


        foreach (Item item in inventory.GetItemList())
        {
            
            if (sprite == item.GetSprite())
            {
                

                if (item.IsCraftItem())
                {
                    craftinventory.AddItem(item);
                    break;
                }

                if (item.IsEatItem())
                {
                    Element newElement = item.GetElement();
                    playercombat.SetElement(newElement);

                    List<Item> removeList = new List<Item>{item};
                    inventory.RemoveItems(removeList);
                    break;
                }

                if (item.IsEquipItem())
                {
                    equipInventory.ChangeEquipItem(item);
                    Debug.Log("Equip");
                    break;
                }

            }
        }

    }




}
