using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private UiInventory uiInventory;

    void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemworld = collider.GetComponent<ItemWorld>();

        if (itemworld != null)
        {
            // Touch item
            inventory.AddItem(itemworld.GetItem());
            itemworld.DestroySelf();
        }
    }

}
