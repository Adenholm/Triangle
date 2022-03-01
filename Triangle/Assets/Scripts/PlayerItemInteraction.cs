using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{

    private Inventory inventory;
    private UiInventory uiInventory;
    private Quest quest;

    void Start()
    {
        uiInventory = GameObject.Find("Inventory").GetComponent<UiInventory>();
        Debug.Log(uiInventory);
        quest = GameObject.Find("Quest").GetComponent<Quest>();
        Debug.Log(quest);
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemworld = collider.GetComponent<ItemWorld>();

        if (itemworld != null)
        {
            // Touch item
            Item item = itemworld.GetItem();
            if (item.itemType == Item.ItemType.Crystal)
            {
                quest.RaiseAmount();
            }
            inventory.AddItem(item);
            itemworld.DestroySelf();
        }
    }

}
