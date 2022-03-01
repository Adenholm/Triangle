using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementHandler
{
    public static ISet<Element> GetBaseElements(Element e)
    {
        switch (e)
        {
            case Element.STEAM: return new HashSet<Element> { Element.WATER, Element.FIRE };
            case Element.POISON: return new HashSet<Element> { Element.WATER, Element.PLANT };
            case Element.FIREWALL: return new HashSet<Element> { Element.FIRE, Element.PLANT };
            case Element.ICE: return new HashSet<Element> { Element.WATER, Element.WIND };
            case Element.LIGHTNING: return new HashSet<Element> { Element.FIRE, Element.WIND };
            case Element.PLANTTHROW: return new HashSet<Element> { Element.WIND, Element.PLANT };

            case Element.FIRE: return new HashSet<Element> { Element.FIRE, Element.FIRE };
            case Element.WATER: return new HashSet<Element> { Element.WATER, Element.WATER };
            case Element.WIND: return new HashSet<Element> { Element.WIND, Element.WIND };
            case Element.PLANT: return new HashSet<Element> { Element.PLANT, Element.PLANT};

            default: Debug.Log("Invalid Element input"); return null;
        }
    }

    public static Element GetElement(ISet<Element> elements)
    {
        if (elements.SetEquals(GetBaseElements(Element.STEAM))) return Element.STEAM;
        else if (elements.SetEquals(GetBaseElements(Element.POISON))) return Element.POISON;
        else if (elements.SetEquals(GetBaseElements(Element.FIREWALL))) return Element.FIREWALL;
        else if (elements.SetEquals(GetBaseElements(Element.ICE))) return Element.ICE;
        else if (elements.SetEquals(GetBaseElements(Element.LIGHTNING))) return Element.LIGHTNING;
        else if (elements.SetEquals(GetBaseElements(Element.PLANTTHROW))) return Element.PLANTTHROW;

        else if (elements.SetEquals(GetBaseElements(Element.FIRE))) return Element.FIRE;
        else if (elements.SetEquals(GetBaseElements(Element.WATER))) return Element.WATER;
        else if (elements.SetEquals(GetBaseElements(Element.WIND))) return Element.WIND;
        else if (elements.SetEquals(GetBaseElements(Element.PLANT))) return Element.PLANT;

        else
        {
            Debug.Log("Invalid Element combination");
            return Element.NONE;
        }
    }

    public static float DamageConverter(Element ownElem, Element damagingElem)
    {
        return 1f;
    }

    public static Sprite GetSprite(Element element)
    {
        switch (element)
        {
            default:
            case Element.FIRE: return ElementAssets.Instance.fireSprite;
            case Element.WATER: return ElementAssets.Instance.waterSprite;
            case Element.WIND: return ElementAssets.Instance.airSprite;
            case Element.PLANT: return ElementAssets.Instance.plantSprite;

            case Element.STEAM: return ElementAssets.Instance.steamSprite;
            case Element.POISON: return ElementAssets.Instance.poisonSprite;
            case Element.LIGHTNING: return ElementAssets.Instance.lightningSprite;
            case Element.PLANTTHROW: return ElementAssets.Instance.plantthrowSprite;
            case Element.ICE: return ElementAssets.Instance.iceSprite;
            case Element.FIREWALL: return ElementAssets.Instance.firewallSprite;
        }
    }

}
