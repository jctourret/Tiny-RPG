using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    protected int hitPoints = 100;
    [SerializeField]
    protected int maxHitPoints = 100;
    protected int staminaPoints = 100;
    protected int maxStaminaPoints = 100;
    protected int staminaRegen = 1;

    int strenght;
    int endurance;
    int agility;

    [SerializeField]
    protected float knockback;
    protected float knockbackResist;
    protected int defense;
    protected bool isBlocking;

    public virtual void SetHitPoints(int hitPoints)
    {
        this.hitPoints = hitPoints;
    }
    public int GetHitPoints()
    {
        return hitPoints;
    }

    public virtual void SetStaminaPoints(int staminaPoints)
    {
        this.staminaPoints = staminaPoints;
    }

    public int GetStaminaPoints()
    {
        return staminaPoints;
    }

    public float GetKnockback()
    {
        return knockback;
    }

    public int GetDefense()
    {
        return defense;
    }

    public void StartBlocking()
    {
        isBlocking = true;
    }

    public void StopBlocking()
    {
        isBlocking = false;
    }

    public bool isDead()
    {
        if (GetHitPoints() <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
