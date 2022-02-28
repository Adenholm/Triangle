using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementInteraction : MonoBehaviour
{
    private Element2 element;
    public ElementUI elementui;

    void Awake()
    {
        element = new Element2();
    }

    public void SetElement(Element2.ElementType newElementType)
    {
        element.elementType = newElementType;
        elementui.SetImage(element);
    }

    public Element2 GetElement()
    {
        return element;
    }
}
