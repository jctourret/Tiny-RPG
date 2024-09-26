using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shield", menuName = "Inventory/Shield")]
public class Shield : Weapon
{
    public static Action OnRaiseShield;
    public static Action OnLowerShield;

    public override void Use()
    {
        OnRaiseShield?.Invoke();
    }

    public override void EndUse()
    {
        OnLowerShield?.Invoke();
    }
}
