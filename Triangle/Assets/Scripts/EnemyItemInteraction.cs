using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemInteraction : MonoBehaviour
{
    private Inventory inventory;

    void Awake()
    {
        inventory = new Inventory();
        inventory.AddItem(new Item { itemType = Item.ItemType.Fur, amount = 2 });
        inventory.AddItem(new Item { itemType = Item.ItemType.Firemeat, amount = 1 });
    }

    /**
     //Test method Dying
     private void Start()
     {
         Dying();
     }*/

    private void Dying()
    {
        Vector3 position = gameObject.transform.position;
        inventory.DropItems(position);
        Destroy(gameObject);
    }
}
