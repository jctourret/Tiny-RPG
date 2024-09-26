using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Consumable
{
    public int healthRestored;

    public override void Use(CharacterStats character)
    {
        character.SetHitPoints(healthRestored + character.GetHitPoints());
    }
}
