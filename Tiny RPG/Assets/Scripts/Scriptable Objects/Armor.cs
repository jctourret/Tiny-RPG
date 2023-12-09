using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipment
{
    ArmorType type;
    public ArmorSlots slot;
}
public enum ArmorType
{
    Clothes,
    Light,
    Medium,
    Heavy
}