using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : object 
{
    public string name;
    public IntPtr others;

    Item(string _name, IntPtr _others) 
    {
        this.name = _name;
        this.others = _others; // <----- I might change this too ui refs or pntrs
    }
}

public sealed class InventorySystem : object
{
    public Dictionary<int, Item> dict = new Dictionary<int, Item>();
}
