using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IsActive : MonoBehaviour
{
    Image backround;

    private void Start()
    {
    Image backround = transform.Find("Backround-slot").GetComponent<Image>();
    }

    public void SetColorActive()
    {
        backround.color = Color.cyan;
    }

    public void SetColorInActive()
    {
        backround.color = Color.clear;
    }
}
