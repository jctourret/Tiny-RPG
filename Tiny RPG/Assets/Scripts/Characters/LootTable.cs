using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LootTable
{
    public Item item;
    public int dropChance;
    public int minDrop;
    public int maxDrop;
}
