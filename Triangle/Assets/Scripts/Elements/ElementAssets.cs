using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAssets : MonoBehaviour
{
    public static ElementAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    public Sprite fireSprite;
    public Sprite waterSprite;
    public Sprite airSprite;
    public Sprite plantSprite;

    public Sprite lightningSprite;
    public Sprite poisonSprite;
    public Sprite iceSprite;
    public Sprite plantthrowSprite;
    public Sprite firewallSprite;
    public Sprite steamSprite;

}
