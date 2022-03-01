using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Element2 : MonoBehaviour
{

    public ElementType elementType;


    public enum ElementType
    {
        Fire,
        Water,
        Air,
        Plant,
        None
    }

    public Sprite GetSprite()
    {
        switch (elementType)
        {
            default:
            case ElementType.Fire: return ElementAssets.Instance.fireSprite;
            case ElementType.Water: return ElementAssets.Instance.waterSprite;
            case ElementType.Air: return ElementAssets.Instance.airSprite;
            case ElementType.Plant: return ElementAssets.Instance.plantSprite;
        }
    }
}
