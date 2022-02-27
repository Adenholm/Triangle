using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    [SerializeField] GameObject inventoryobj;
    [SerializeField] GameObject craftinventoryobj;
    GameObject backroundInventory;
    GameObject containerInventory;
    GameObject craftInventory;

    private void Start()
    {
        backroundInventory = inventoryobj.transform.GetChild(0).gameObject;
        containerInventory = inventoryobj.transform.GetChild(1).gameObject;
        craftInventory = craftinventoryobj.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            backroundInventory.SetActive(!backroundInventory.activeSelf);
            containerInventory.SetActive(!containerInventory.activeSelf);
            craftInventory.SetActive(!craftInventory.activeSelf);
        }
    }
}
