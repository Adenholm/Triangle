using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUI : MonoBehaviour
{
    public void SetImage(Element2 element)
    {
        Image image = transform.Find("image").GetComponent<Image>();
        
        image.sprite = element.GetSprite();
    }
}
