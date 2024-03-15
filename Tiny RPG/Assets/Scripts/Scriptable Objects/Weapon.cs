using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{
    public AnimatorOverrideController overrideController;
    [Header("Placement & collision")]
    public Vector3 posOffset;
    public Vector3 rotOffset;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
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
