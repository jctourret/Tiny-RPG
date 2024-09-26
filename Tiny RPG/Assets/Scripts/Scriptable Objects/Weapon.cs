using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{

    public enum WeaponCollider
    {
        Box,
        Capsule
    }

    public AnimatorOverrideController overrideController;
    [Header("Transform")]
    public Vector3 posOffset;
    public float rotOffset;

    [Header("Collider")]
    public WeaponCollider collider;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;

    public virtual void Use()
    {

    }

    public virtual void EndUse()
    {

    }
}