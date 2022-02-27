using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonCraftItem : MonoBehaviour
{
    [SerializeField] Image resultImage;
    [SerializeField] CraftInventory craftInventory;
    public PlayerItemInteraction player;
    public Inventory inventory;
    public void RemoveCraftItem()
    {
        Image image = transform.Find("image").GetComponent<Image>();
        Sprite sprite = image.sprite;
        List <Item> craftList = craftInventory.GetCraftList();

        foreach (int i in Enumerable.Range(0,craftList.Count))
        {
            if (sprite == craftList[i].GetSprite())
            {
                craftInventory.RemoveItem(i);
                break;
            }
        }

        image.gameObject.SetActive(false);
        resultImage.gameObject.SetActive(false);

    }

    public void CommitCrafting()
    {
        inventory = player.GetInventory();
        Sprite sprite = resultImage.sprite;
        if(sprite == ItemAssets.Instance.swordSprite)
        {
            List<Item> craftList = craftInventory.GetCraftList();
            inventory.AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
            inventory.RemoveItems(craftList);
            craftInventory.RemoveItem(0);
            craftInventory.RemoveItem(0);
            craftInventory.HideSlots();
            

        }
        if (sprite == ItemAssets.Instance.bowSprite)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.Bow, amount = 1 });
            craftInventory.RemoveItem(0);
            craftInventory.RemoveItem(0);
            craftInventory.HideSlots();
        }
        if (sprite == ItemAssets.Instance.armourSprite)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.Armour, amount = 1 });
            craftInventory.RemoveItem(0);
            craftInventory.RemoveItem(0);
            craftInventory.HideSlots();
        }
    }
}
