using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventory : MonoBehaviour
{
    private Item weaponitem;
    private Item armouritem;
    public UiEquip uiequip;

    public EquipInventory()
    {
        weaponitem = new Item { itemType = Item.ItemType.None};
        armouritem = new Item { itemType = Item.ItemType.None };
    }

    public Item GetWeaponItem()
    {
        return weaponitem;
    }

    public Item GetArmourItem()
    {
        return armouritem;
    }

    public void ChangeEquipItem(Item item)
    {
        if (item.itemType == Item.ItemType.Sword | item.itemType == Item.ItemType.Bow)
        {
            weaponitem = item;
            uiequip.ShowWeaponItem(item);
            PlayerCombat pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            pc.EquipWeapon();
            //change weopon on player
        }
        
        if (item.itemType == Item.ItemType.Armour)
        {
            armouritem = item;
            uiequip.ShowArmourItem(item);
            PlayerCombat pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            pc.armorIsEquiped = true;
            //change armour on player
        }
    }

}
