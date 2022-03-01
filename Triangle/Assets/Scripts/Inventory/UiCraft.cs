using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCraft : MonoBehaviour
{

    private Transform itemCraftContainer;
    private Transform craftSlot1;
    private Transform craftSlot2;
    private Transform craftSlotresult;
    private Image image1;
    private Image image2;
    private Image imageresult;
    private Transform button1;
    private Transform button2;
    private Transform buttonfinal;

    private void Start()
    {
        itemCraftContainer = transform.Find("itemCraftContainer");
        craftSlot1 = itemCraftContainer.Find("craftSlot1");
        craftSlot2 = itemCraftContainer.Find("craftSlot2");
        craftSlotresult = itemCraftContainer.Find("craftSlotresult");

        //image1 = craftSlot1.Find("image").GetComponent<Image>();
        //image2 = craftSlot2.Find("image").GetComponent<Image>();
        //imageresult = craftSlotresult.Find("image").GetComponent<Image>();
        button1 = craftSlot1.Find("Button");
        button2 = craftSlot2.Find("Button");
        buttonfinal = craftSlotresult.Find("Button");

        image1 = button1.transform.GetChild(0).GetComponent<Image>();
        image2 = button2.transform.GetChild(0).GetComponent<Image>();
        imageresult = buttonfinal.transform.GetChild(0).GetComponent<Image>();
    }


    public void ShowCraftSlotItem(List<Item> craftList)
    {
        image1.sprite = craftList[0].GetSprite();
        image1.gameObject.SetActive(true);
        if (craftList.Count>1)
        {
            image2.sprite = craftList[1].GetSprite();
            image2.gameObject.SetActive(true);
        }

    }

    public void ShowResultItem(Item item)
    {
        imageresult.sprite = item.GetSprite();
        imageresult.gameObject.SetActive(true);
       
    }

    public void HideItemSlot1()
    {
        image1.gameObject.SetActive(false);
    }
    public void HideItemSlot2()
    {
        image2.gameObject.SetActive(false);
    }
    public void HideResultImage()
    {
        imageresult.gameObject.SetActive(false);
    }
}

