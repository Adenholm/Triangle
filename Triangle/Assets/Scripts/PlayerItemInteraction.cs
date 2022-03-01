using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private UiInventory uiInventory;

    public Quest quest;

    void Awake()
    {
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
