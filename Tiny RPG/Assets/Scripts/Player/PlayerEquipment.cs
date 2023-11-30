using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Armor[] armorSlots;

    public Weapon[] weaponSlots;
    [SerializeField]
    Hand main;
    [SerializeField]
    Hand off;


    // Start is called before the first frame update
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(ArmorSlots)).Length;
        armorSlots = new Armor[numSlots];
    }

    public void Equip(Equipment equipment)
    {

    }

    public void EquipArmor(Armor armor)
    {

    }

    public void EquipWeapon(Weapon weapon)
    {

    }
    public void UseMainHand()
    {
        main.Use();
    }
    public void UseOffHand()
    {
        off.Use();
    }
}

public enum ArmorSlots
{
    Helmet,
    Chest,
    Legs,
    Arms,
    Feet
}
public enum WeaponSlot
{
    MainHand,
    OffHand,
}