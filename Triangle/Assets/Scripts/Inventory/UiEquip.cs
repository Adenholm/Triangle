using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEquip : MonoBehaviour
{
    private Transform itemEquipContainer;
    private Transform equipSlot1;
    private Transform equipSlot2;

    private Image image1;   //weapon
    private Image image2;   //armour

    private void Start()
    {
        itemEquipContainer = transform.Find("itemEquipContainer");
        equipSlot1 = itemEquipContainer.Find("equipSlot1");
        equipSlot2 = itemEquipContainer.Find("equipSlot2");
        image1 = equipSlot1.Find("image").GetComponent<Image>();
        image2 = equipSlot2.Find("image").GetComponent<Image>();
    }

    public void ShowWeaponItem(Item item)
    {
        image1.sprite = item.GetSprite();
        image1.gameObject.SetActive(true);
    }

    public void ShowArmourItem(Item item)
    {
        image2.sprite = item.GetSprite();
        image2.gameObject.SetActive(true);
    }


}
