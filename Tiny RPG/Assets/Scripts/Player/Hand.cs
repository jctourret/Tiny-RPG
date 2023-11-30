using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;
    Weapon equippedWeapon;
    bool isAttacking;
    float useTimer;
    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
}
