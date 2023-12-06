using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{
    public AnimatorOverrideController overrideController;
    public WeaponSlot slot;
    bool isAttacking;
    public override void Use()
    {

    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }
}
