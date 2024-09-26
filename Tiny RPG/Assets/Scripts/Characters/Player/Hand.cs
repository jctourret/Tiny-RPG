using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class Hand : MonoBehaviour
{
    HandView hView;
    float useTimer;
    [SerializeField]
    public Weapon currentWeapon;
    Camera cam;

    float clampMin = -25;
    float clampMax = 60;
    float angle;

    public float aimRadius = 0.1f;

    [SerializeField]
    float handRange = 0.5f;

    public bool followsMouse;

    private void OnEnable()
    {
        HandView.OnStartWeaponEvent += StartWeaponEvent;
        HandView.OnEndWeaponEvent += EndWeaponEvent;
    }

    private void OnDisable()
    {
        HandView.OnStartWeaponEvent -= StartWeaponEvent;
        HandView.OnEndWeaponEvent -= EndWeaponEvent;
    }


    public void Start()
    {
        hView = GetComponentInChildren<HandView>();
        cam = Camera.main;
        if(currentWeapon != null)
        {
            Equip(currentWeapon);
        }
    }
    private void LateUpdate()
    {
        useTimer += Time.deltaTime;
        if (followsMouse)
        {
            //AngleBasedFollowMouse();
            FollowMouse();
        }
    }

    public void Use()
    {
        if (useTimer >= hView.GetCurrentAnimState())
        {
            hView.StartUse();
            useTimer = 0f;
        }
    }

    public void StartWeaponEvent(HandView eventEmitter)
    {
        if (hView == eventEmitter)
        {
            currentWeapon.Use();
        }
    }

    public void EndWeaponEvent(HandView eventEmitter)
    {
        if (hView == eventEmitter)
        {
            currentWeapon.EndUse();
        }
    }
    public void FollowMouse()
    {
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);


        Vector2 mouseRelative = (mouseWorldPosition - transform.parent.position);

        Vector2 aimdirection = mouseRelative.normalized;

        Vector2 parentPos = new Vector2(transform.parent.position.x, transform.parent.position.y);

        angle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg;

        if(mouseRelative.magnitude < handRange)
        {
            transform.position = mouseRelative + parentPos;
        }
        else
        {
            transform.position = (mouseRelative.normalized * handRange) + parentPos;
        }

        if (angle < 90 && angle > -90)
        {
            transform.rotation = Quaternion.AngleAxis(angle - currentWeapon.rotOffset, Vector3.forward);
        }
        else
        {
            angle = Mathf.Atan2(-aimdirection.y, -aimdirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - currentWeapon.rotOffset, Vector3.forward);
        }
    }
    public void Equip(Weapon newWeapon)
    {
        //Cambio de Arma
        currentWeapon = newWeapon;
        //Updateo el view.
        hView.UpdateHand(currentWeapon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hand Triggered by " + collision.name);
        IDamageable damageable = collision.gameObject.GetComponentInChildren<IDamageable>();
        if (damageable != null)
        {
            Vector3 knockbackDirection = collision.gameObject.transform.position - transform.position;
            knockbackDirection.Normalize();

            Vector3 attackKnockback = knockbackDirection * currentWeapon.GetKnockback();
            damageable.TakeDamage(currentWeapon.damageMod,attackKnockback);
        }
    }
}
