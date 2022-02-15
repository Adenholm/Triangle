using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementHandler : MonoBehaviour
{
    ISet<Element> GetBaseElements(Element e)
    {
        switch (e)
        {
            case Element.STEAM: return new HashSet<Element> { Element.WATER, Element.FIRE };
            case Element.POISON: return new HashSet<Element> { Element.WATER, Element.PLANT };
            case Element.FIREWALL: return new HashSet<Element> { Element.FIRE, Element.PLANT };
            case Element.ICE: return new HashSet<Element> { Element.WATER, Element.WIND };
            case Element.LIGHTNING: return new HashSet<Element> { Element.FIRE, Element.WIND };
            case Element.PLANTTHROW: return new HashSet<Element> { Element.WIND, Element.PLANT };
            default: throw new System.Exception("Invalid Element input");
        }
    }

    Element GetElement(ISet<Element> elements)
    {
        if (elements.SetEquals(GetBaseElements(Element.STEAM))) return Element.STEAM;
        else if (elements.SetEquals(GetBaseElements(Element.POISON))) return Element.POISON;
        else if (elements.SetEquals(GetBaseElements(Element.FIREWALL))) return Element.FIREWALL;
        else if (elements.SetEquals(GetBaseElements(Element.ICE))) return Element.ICE;
        else if (elements.SetEquals(GetBaseElements(Element.LIGHTNING))) return Element.LIGHTNING;
        else if (elements.SetEquals(GetBaseElements(Element.PLANTTHROW))) return Element.PLANTTHROW;
        else throw new System.Exception("Invalid Element combination");
    }
}
