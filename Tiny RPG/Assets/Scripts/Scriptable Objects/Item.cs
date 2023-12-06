using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    public static Action<Item> OnItemPickUp;
    new public string name;
    public Sprite sprite;
    public int value;
    public int weight;

    public virtual void Use()
    {
        OnItemPickUp?.Invoke(this);
    }
}
