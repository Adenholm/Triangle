using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    public Transform pfCollectibles;

    public Sprite woodSprite;
    public Sprite ironSprite;
    public Sprite furSprite;
    public Sprite crystalSprite;
    public Sprite swordSprite;
    public Sprite bowSprite;
    public Sprite armourSprite;
    public Sprite firemeatSprite;
    public Sprite watermeatSprite;
    public Sprite airmeatSprite;
    public Sprite plantmeatSprite;


}
