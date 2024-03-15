using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{   
    public enum EquipmentSlots
    {
        Helmet,
        Chest,
        Legs,
        Arms,
        Feet,
        MainHand,
        OffHand
    }
    [Header("Statistics")]
    public int damageMod;
    public int defenseMod;
    public EquipmentSlots slot;
    public override void Use()
    {

    }
}
