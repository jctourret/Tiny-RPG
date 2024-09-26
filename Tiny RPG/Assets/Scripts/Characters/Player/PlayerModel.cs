using System;
using UnityEngine;

public class PlayerModel : CharacterStats
{
    public static Action<int, Vector3> OnPlayerBlock;
    public static Action<int,Vector3> OnPlayerHit;
    public static Action<int> OnPlayerHPChange;
    public static Action<int> OnPlayerSTChange;
    public static Action OnPlayerDeath;

    PlayerEquipment equipment;

    private void OnEnable()
    {
        Shield.OnRaiseShield += StartBlocking;
        Shield.OnLowerShield += StopBlocking;
    }


    private void OnDisable()
    {
        Shield.OnRaiseShield -= StartBlocking;
        Shield.OnRaiseShield -= StopBlocking;
    }


    private void Start()
    {
        equipment = GetComponent<PlayerEquipment>();
    }
    public void TakeDamage(int damage, Vector3 knockback)
    {
        Console.Write("Player Damaged");


        if (isBlocking)
        {
            if(GetStaminaPoints() - damage < 0)
            {
                int remainingDamage = GetStaminaPoints() - damage;
                SetStaminaPoints(0);
                SetHitPoints(remainingDamage);
                OnPlayerHit?.Invoke(GetHitPoints(), knockback);
            }
            else
            {
                SetStaminaPoints(GetStaminaPoints() - damage);
                OnPlayerBlock?.Invoke(GetHitPoints(), knockback);
            }
        }
        else
        {
            SetHitPoints(GetHitPoints() - damage);
            OnPlayerHit?.Invoke(GetHitPoints(), knockback);
        }
        if (isDead()){
            KillPlayer();
        }
    }
    public void KillPlayer()
    {
        OnPlayerDeath?.Invoke();
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void UseMainHand()
    {
        equipment.UseMainHand();
    }

    public void UseOffHand()
    {
        equipment.UseOffHand();
    }

    public void RegenStamina()
    {
        SetStaminaPoints(GetStaminaPoints() + staminaRegen);
    }

    public override void SetHitPoints(int hitPoints)
    {
        base.SetHitPoints(hitPoints);
        OnPlayerHPChange?.Invoke(GetHitPoints());
    }

    public override void SetStaminaPoints(int staminaPoints)
    {
        base.SetStaminaPoints(staminaPoints);
        OnPlayerSTChange?.Invoke(GetHitPoints());
    }
}
