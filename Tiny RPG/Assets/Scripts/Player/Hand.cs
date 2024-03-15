using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;
    HandView hView;
    float useTimer;
    CapsuleCollider2D equippedCol;
    Weapon currentWeapon;
    Camera cam;

    float clampMin = -25;
    float clampMax = 60;
    float angle;
    float angleAbs;
    public bool followsMouse;
    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hView = GetComponentInChildren<HandView>();
        equippedCol = GetComponentInChildren<CapsuleCollider2D>();
        equippedCol.enabled = false;
        cam = Camera.main;
    }
    private void Update()
    {
        useTimer += Time.deltaTime;
        if (followsMouse)
        {
            FollowMouse();
        }
    }
    public void Use()
    {
        if (useTimer >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            animator.SetTrigger("Use");
            useTimer = 0f;
        }
    }
    public void FollowMouse()
    {
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimdirection = (mouseWorldPosition - transform.position).normalized;
        angle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg;
        if (angle < 90 && angle > -90)
        {
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(angle, clampMin, clampMax));
        }
        else
        {
            angle = Mathf.Atan2(-aimdirection.y, -aimdirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(angle, -clampMax, -clampMin));
        }
    }
    public void Equip(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        UpdateHand();
    }

    public void UpdateHand()
    {
        animator.runtimeAnimatorController = currentWeapon.overrideController;
        hView.transform.position = currentWeapon.posOffset;
        hView.transform.rotation = Quaternion.Euler(currentWeapon.rotOffset);
        hView.spriteRenderer.sprite = currentWeapon.sprite;
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
