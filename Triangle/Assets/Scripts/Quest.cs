using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    TextMeshProUGUI text;
    int amount;
    public int amountgoal;

    public GameObject map;

    void Start()
    {
        amount = 0;
        text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        text.SetText("Crystals: " + amount +"/"+amountgoal);
    }

    public void RaiseAmount()
    {
        amount += 1;
        text.SetText("Crystals: " + amount + "/" + amountgoal);

        if (amount == amountgoal)
        {
            map.GetComponent<MapGenerator>().GenerateMap();
            Debug.Log("The End");
        }
    }
}
