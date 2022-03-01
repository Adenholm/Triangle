using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
    TextMeshProUGUI text;
    int amount;
    public int amountgoal;

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
            Debug.Log("The End");
        }
    }
}
