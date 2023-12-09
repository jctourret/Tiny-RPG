using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static Action<List<Item>,int> OnPlayerInvInit;
    public static Action<List<Item>> OnPlayerInvUpdate;
    public static Action OnInventoryUIToggle;
    public static Action<GameObject, Item> OnItemStolen;

    List<Item> inventory = new List<Item>();
    [SerializeField][Range(1,10)]
    int inventorySpace;
    private void OnEnable()
    {
        WorldItem.OnItemPickUp += Add;
    }

    private void OnDisable()
    {
        WorldItem.OnItemPickUp -= Add;
    }
    private void Start()
    {
        OnPlayerInvInit?.Invoke(inventory,inventorySpace);
    }
    public void ToggleInventoryUI()
    {
        OnInventoryUIToggle?.Invoke();
    }
    public bool Add(Item newItem, GameObject interactor)
    {
        CheckOwnership(newItem,interactor);
        if(inventory.Count < inventorySpace) //Tengo espacio en el inventario?
        {
            Item existingItem = null;
            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i].id == newItem.id) //Tengo un item con este ID?
                {
                    if(inventory[i].stack < inventory[i].stackLimit) // Ese item tiene espacio en su stack?
                    {
                        existingItem = inventory[i]; // Si existe un item con el mismo ID y tiene espacio en su stack, me lo guardo.
                        break; //Salgo del for al primero que encuentre.
                    }
                }
            }

            if (existingItem != null) //Al final, encontre algun item?
            {
                existingItem.stack++;
            }
            else
            {
                inventory.Add(newItem);
            }
            OnPlayerInvUpdate?.Invoke(inventory);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckOwnership(Item item,GameObject interactor)
    {
        if (item.owner == null)
        {
            item.owner = gameObject;
        }
        else if (item.owner != interactor)
        {
            OnItemStolen?.Invoke(interactor,item);
        }
        
    }

    public void Remove(Item itemToRemove)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == itemToRemove.id) //Tengo un item con este ID?
            {
                if (inventory[i].stack > 0) // Ese item tiene por lo menos un objeto en el stack?
                {
                    inventory[i].stack--;
                }
                else
                {
                    inventory.Remove(inventory[i]);
                }
                break; //Salgo del for al primero que encuentre.
            }
        }
    }

}
