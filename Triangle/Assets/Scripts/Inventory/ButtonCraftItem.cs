using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonCraftItem : MonoBehaviour
{
    [SerializeField] Image resultImage;
    [SerializeField] CraftInventory craftInventory;
    private PlayerItemInteraction playeritem;
    public Inventory inventory;
    Item resultitem;

    private void Start()
    {
        playeritem = GameObject.Find("Player 1(Clone)").GetComponent<PlayerItemInteraction>();
    }

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
        inventory = playeritem.GetInventory();
        List<Item> craftList = craftInventory.GetCraftList();
        Sprite sprite = resultImage.sprite;
        

        if (sprite == ItemAssets.Instance.swordSprite)
        {
            resultitem = new Item { itemType = Item.ItemType.Sword, amount = 1 };
        }
        if (sprite == ItemAssets.Instance.bowSprite)
        {
            resultitem = new Item { itemType = Item.ItemType.Bow, amount = 1 };
        }
        if (sprite == ItemAssets.Instance.armourSprite)
        {
            resultitem = new Item { itemType = Item.ItemType.Armour, amount = 1 };
        }

        inventory.RemoveItems(craftList);

        foreach (int i in Enumerable.Range(0, craftList.Count))
        {
            craftInventory.RemoveItem(0);
        }

        craftInventory.HideSlots();
        inventory.AddItem(resultitem);
    } 
}
