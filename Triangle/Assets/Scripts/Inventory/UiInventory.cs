using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1 });
        inventory.AddItem(new Item { itemType = Item.ItemType.Iron, amount = 1 }); ;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
       
        foreach (Transform child in itemSlotContainer)
        {
           
            if( child == itemSlotTemplate) continue;
            Destroy(child.gameObject);

        }

        int x = 0;
        int y = 0;
        float itemSlotCellsize = 35f;

        foreach (Item item in inventory.GetItemList())
        {
            
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellsize, -y * itemSlotCellsize);

            //Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            Image button = itemSlotRectTransform.Find("Button").GetComponent<Image>();
            Image image = button.transform.GetChild(0).GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uitext = itemSlotRectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uitext.SetText(item.amount.ToString());
            }
            else
            {
                uitext.SetText("");
            }

            x++;
            if (x > 3){
                x = 0;
                y++;
            }

        }
    }
}
