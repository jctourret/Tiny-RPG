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
    [SerializeField]
    IInteractable interactable;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by " + other.name);
        if(interactable == null)
        {
            interactable = other.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.ShowPrompt();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(interactable != null)
        {
            interactable.HidePrompt();
            interactable = null;
        }
    }
}
