using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    private Item item;
    public Item.ItemType itemType;
    public int amount;

    private void Start()
    {
       item = new Item();

       item.amount = amount;
       item.itemType = itemType;
        
        ItemWorld.SpawnItemWorld(gameObject.transform.position, item);
        Destroy(gameObject);
    }
}
