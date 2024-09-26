using System;
using UnityEngine;

public class HandView : MonoBehaviour
{
    public static Action<HandView> OnStartWeaponEvent;
    public static Action<HandView> OnEndWeaponEvent;

    Animator animator;
    Collider2D equippedCol;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        equippedCol = GetComponent<Collider2D>();
        equippedCol.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartUse()
    {
        animator.SetTrigger("Use");
    }

    public void UpdateHand(Weapon newWeapon)
    {
        SetAnimatorController(newWeapon.overrideController);
        spriteRenderer.sprite = newWeapon.sprite;
        // Get all colliders attached to this GameObject
        Collider[] colliders = gameObject.GetComponents<Collider>();

        // Loop through each collider and destroy it
        foreach (Collider collider in colliders)
        {
            Destroy(collider);
        }

        switch (newWeapon.collider)
        {
            case Weapon.WeaponCollider.Box:
                BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
                box.offset = newWeapon.colliderOffset;
                box.size = newWeapon.colliderSize;
                equippedCol = box;
                break;
            case Weapon.WeaponCollider.Capsule:
                CapsuleCollider2D capsule = gameObject.AddComponent<CapsuleCollider2D>();
                capsule.offset = newWeapon.colliderOffset;
                capsule.size = newWeapon.colliderSize;
                equippedCol = capsule;
                break;
            default:
                Debug.Log("Non-valid collider");
                break;
        }
        equippedCol.enabled = false;
        equippedCol.isTrigger = true;
    }
    public float GetCurrentAnimState()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void SetAnimatorController(AnimatorOverrideController animOverride)
    {
        animator.runtimeAnimatorController = animOverride;
    }

    public void StartWeaponEvent()
    {
        equippedCol.enabled = true;
        OnStartWeaponEvent?.Invoke(this);
    }
    public void EndWeaponEvent()
    {
        equippedCol.enabled = false;
        OnEndWeaponEvent?.Invoke(this);
    }

    
}
