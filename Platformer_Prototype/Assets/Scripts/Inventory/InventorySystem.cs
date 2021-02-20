using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ItemStats 
{
    int damage, health;
};

public enum ItemEnum 
{
    POTIONS, 
    WEAPONS,
    BOOSTS, 
    OTHERS
};

public class Item : object 
{
    public string name;
    public GameObject objref;
    public RawImage rawimage;
    public ItemEnum ienum;
    public ItemStats istats;

    public Item(string _name, ItemEnum _ienum, ItemStats _istats, GameObject _objref = null, RawImage _rawimage = null) 
    {
        this.name = _name;
        this.objref = _objref;
        this.rawimage = _rawimage;
        this.ienum = _ienum;
        this.istats = _istats;
    }
};

public sealed class InventorySystem : object
{
    public SortedDictionary<int, Item> dict = new SortedDictionary<int, Item>();

    public InventorySystem() 
    {
        
    }
};
