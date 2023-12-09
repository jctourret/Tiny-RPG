using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;
    float useTimer;
    Collider2D equippedCol;
    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        equippedCol = GetComponentInChildren<Collider2D>();
        equippedCol.enabled = false;
    }
    private void Update()
    {
        useTimer += Time.deltaTime;   
    }
    public void Use()
    {
        if (useTimer >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetTrigger("Use");
            useTimer = 0f;
        }
    }

    public void Equip(Weapon newWeapon)
    {
        animator.runtimeAnimatorController = newWeapon.overrideController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hand Triggered by " + collision.name);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage();
        }
    }
    public void StartAttack()
    {
        equippedCol.enabled = true;
    }
    public void EndAttack()
    {
        equippedCol.enabled = false;
    }
}
