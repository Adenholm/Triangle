using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Item : MonoBehaviour
{

    public ItemType itemType;
    public int amount;

  public enum ItemType
    {
        Wood,
        Iron,
        Fur,
        Crystal,
        Sword,
        Bow,
        Armour,
        Firemeat,
        Watermeat,
        Airmeat,
        Plantmeat
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Wood: return ItemAssets.Instance.woodSprite;
            case ItemType.Iron: return ItemAssets.Instance.ironSprite;
            case ItemType.Fur: return ItemAssets.Instance.furSprite;
            case ItemType.Crystal: return ItemAssets.Instance.crystalSprite;
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.Bow: return ItemAssets.Instance.bowSprite;
            case ItemType.Armour: return ItemAssets.Instance.armourSprite;
            case ItemType.Firemeat: return ItemAssets.Instance.firemeatSprite;
            case ItemType.Watermeat: return ItemAssets.Instance.watermeatSprite;
            case ItemType.Airmeat: return ItemAssets.Instance.airmeatSprite;
            case ItemType.Plantmeat: return ItemAssets.Instance.plantmeatSprite;
        }
    }

    
    public bool IsCraftItem()
    {
        switch (itemType)
        {
        default:
        case ItemType.Wood:
        case ItemType.Iron:
        case ItemType.Fur:
            return true;
        case ItemType.Crystal:
        case ItemType.Firemeat:
        case ItemType.Watermeat:
        case ItemType.Airmeat:
        case ItemType.Plantmeat:
        case ItemType.Sword:
        case ItemType.Bow:
        case ItemType.Armour:
            return false;
        }
        
    }

    public bool IsEatItem()
    {
        switch (itemType)
        {
            default:
            case ItemType.Firemeat:
            case ItemType.Watermeat:
            case ItemType.Airmeat:
            case ItemType.Plantmeat:
                return true;
            case ItemType.Wood:
            case ItemType.Iron:
            case ItemType.Fur:
            case ItemType.Crystal:
            case ItemType.Sword:
            case ItemType.Bow:
            case ItemType.Armour:
                return false;
        }

    }
    public bool IsEquipItem()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
            case ItemType.Bow:
            case ItemType.Armour:
                return true;
            case ItemType.Wood:
            case ItemType.Iron:
            case ItemType.Fur:
            case ItemType.Crystal:
            case ItemType.Firemeat:
            case ItemType.Watermeat:
            case ItemType.Airmeat:
            case ItemType.Plantmeat:
                return false;
        }

    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Wood:
            case ItemType.Iron:
            case ItemType.Fur:
            case ItemType.Crystal:
            case ItemType.Firemeat:
            case ItemType.Watermeat:
            case ItemType.Airmeat:
            case ItemType.Plantmeat:
                return true;
            case ItemType.Sword:
            case ItemType.Bow:
            case ItemType.Armour:
                return false;
        }
    }
}
