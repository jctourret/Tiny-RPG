using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static Action OnInventoryUIToggle;

    PlayerLocomotion locomotion;
    PlayerModel model;
    PlayerView view;

    [SerializeField]
    IInteractable interactable;


    
    void Start()
    {
        locomotion = GetComponent<PlayerLocomotion>();
        model = GetComponentInChildren<PlayerModel>();
        view = GetComponentInChildren<PlayerView>();
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
            model.UseMainHand();
        }
    }
    private void OffHandInput()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            model.UseOffHand();
        }
    }

    public void ToggleInventoryUI()
    {
        OnInventoryUIToggle?.Invoke();
    }

    public void StartBlocking()
    {
        model.StartBlocking();
    }

    public void StopBlocking()
    {
        model.StopBlocking();
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
                interactable.EnterInteractionRange();
            }
        }
        else
        {
            IInteractable newInteractable;
            newInteractable = other.gameObject.GetComponent<IInteractable>();
            if (newInteractable != null)
            {
                interactable.ExitInteractionRange();
                interactable = newInteractable;
                Debug.Log("Showing " + other.name + "Prompt");
                interactable.EnterInteractionRange();
            }
        }
    }

    public void TakeDamage(int damage, Vector3 knockback)
    {
        model.TakeDamage(damage, knockback);
        view.TriggerHurt();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable != null && interactable == collision.GetComponent<IInteractable>())
        {
            Debug.Log("Showing " + collision.name + "Prompt");
            interactable.ExitInteractionRange();
            interactable = null;
        }
    }
}
