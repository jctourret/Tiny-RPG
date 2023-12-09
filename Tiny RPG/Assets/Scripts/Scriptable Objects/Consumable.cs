using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public static Action<Item> OnConsumableEmpty;
    public override void Use()
    {
        if(stack-1 <= 0)
        {
            OnConsumableEmpty?.Invoke(this);
        }
        else
        {
            stack--;
        }
    }
}
