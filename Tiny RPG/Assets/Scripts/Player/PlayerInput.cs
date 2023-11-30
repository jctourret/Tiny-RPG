using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerLocomotion locomotion;
    PlayerEquipment equipment;

    GameObject mainHand;
    GameObject offHand;

    // Start is called before the first frame update
    void Start()
    {
        locomotion = GetComponent<PlayerLocomotion>();
        equipment = GetComponentInChildren<PlayerEquipment>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        MainHandInput();
        OffHandInput();
    }
    void MoveInput()
    {
        locomotion.SetDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        locomotion.SetSprinting(Input.GetButton("Sprint"));
    }
    void MainHandInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            equipment.UseMainHand();
        }
    }
    private void OffHandInput()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            equipment.UseOffHand();
        }
    }

}
