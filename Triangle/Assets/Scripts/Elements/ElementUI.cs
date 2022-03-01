using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUI : MonoBehaviour
{
    private List<Element> baseElementsList;
    private Image finalimage;
    private Image image1;
    private Image image2;

    private void Awake()
    {
        baseElementsList = new List<Element>();
        finalimage = transform.Find("finalslotelement-image").GetComponent<Image>();
        image1 = transform.Find("slot1element-image").GetComponent<Image>();
        image2 = transform.Find("slot2element-image").GetComponent<Image>();
    }
    public void SetBaseElementImage(Element element)
    { 
        if (baseElementsList.Count == 2)
        {
            baseElementsList.RemoveAt(0);
        }
        
        baseElementsList.Add(element);
        
        if (baseElementsList.Count >= 1)
        {
            image1.sprite = ElementHandler.GetSprite(baseElementsList[0]);
            image1.gameObject.SetActive(true);
        }
        if (baseElementsList.Count == 2)
        {
            image2.sprite = ElementHandler.GetSprite(baseElementsList[1]);
            image2.gameObject.SetActive(true);
        }
       
    }

    public List<Element> GetBaseElements()
    {
        return baseElementsList;
    }

    public void SetActivElementImage(Element activeElement)
    {
        finalimage.sprite = ElementHandler.GetSprite(activeElement);
        finalimage.gameObject.SetActive(true);
    }

}
