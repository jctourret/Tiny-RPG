using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerLocomotion locomotion;
    PlayerEquipment equipment;
    public static Action OnInventoryUIToggle;

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
        InventoryInput();
        MainHandInput();
        OffHandInput();
    }
    void MoveInput()
    {
        locomotion.SetDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        locomotion.SetSprinting(Input.GetButton("Sprint"));
    }
    void InventoryInput()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventoryUI();
        }
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

    public void ToggleInventoryUI()
    {
        OnInventoryUIToggle?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by " + other.name);
        if(interactable == null)
        {
            interactable = other.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("Showing " + other.name + "Prompt");
                interactable.ShowPrompt();
            }
        }
        else
        {
            IInteractable newInteractable;
            newInteractable = other.gameObject.GetComponent<IInteractable>();
            if (newInteractable != null)
            {
                interactable.HidePrompt();
                interactable = newInteractable;
                Debug.Log("Showing " + other.name + "Prompt");
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
                Debug.Log("Interacting with "+ other.name);
                interactable.Interact(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name + "has exitted trigger.");
        if (interactable != null && interactable == collision.GetComponent<IInteractable>())
        {
            Debug.Log("Showing " + collision.name + "Prompt");
            interactable.HidePrompt();
            interactable = null;
        }
    }
}
