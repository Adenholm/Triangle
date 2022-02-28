using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInventory : MonoBehaviour
{
    
    public PlayerItemInteraction playeritem;
    public PlayerElementInteraction playerelement;
    
    public CraftInventory craftinventory;

    private Inventory inventory;

    public void UseItem()
    {
        inventory = playeritem.GetInventory();

        //Get itemType of clicked Image
        Image image = transform.Find("image").GetComponent<Image>();
        Image backround = transform.Find("Backround-slot").GetComponent<Image>();
        Sprite sprite = image.sprite;
       // IsActive active = gameObject.GetComponent<IsActive>();


        foreach (Item item in inventory.GetItemList())
        {
            
            if (sprite == item.GetSprite())
            {
                

                if (item.IsCraftItem())
                {
                    //active.SetColorActive();
                    craftinventory.AddItem(item);
                    break;
                }

                if (item.IsEatItem())
                {
                    Debug.Log("Eat");
                    Element2.ElementType newElementType = item.GetElementType();
                    playerelement.SetElement(newElementType);

                    //elementui.RefreshUi();
                    
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
