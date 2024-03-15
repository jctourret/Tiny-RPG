using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    Armor[] armorSlots;

    [SerializeField]
    Hand main;
    [SerializeField]
    Hand off;

    private void OnEnable()
    {
        EquipmentSlot.OnItemEquipped += Equip;
    }

    private void OnDisable()
    {
        EquipmentSlot.OnItemEquipped -= Equip;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Equip(Equipment equipment)
    {
        switch (equipment.slot)
        {
            case Equipment.EquipmentSlots.MainHand:
                EquipWeapon(equipment as Weapon, main);
                break;
            case Equipment.EquipmentSlots.OffHand:
                EquipWeapon(equipment as Weapon, off);
                break;
            default:
                EquipArmor(equipment as Armor);
                break;
        }
    }

    public void EquipArmor(Armor armor)
    {
        armorSlots[(int)armor.slot] = armor;
    }

    public void EquipWeapon(Weapon weapon, Hand hand)
    {
        hand.Equip(weapon);
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
