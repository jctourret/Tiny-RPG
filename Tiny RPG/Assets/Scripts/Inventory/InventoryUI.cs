using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static Action<GameObject, Item> OnItemStolen;
    public static Action<int> OnWealthUpdate;

    const int maxColumns = 5;
    const int maxRows = 5;

    [Header("Distribution")]
    [SerializeField]
    float cellSizeX = 120f;
    [SerializeField]
    float cellSizeY = 120f;

    [SerializeField]
    RectTransform container;
    [SerializeField]
    RectTransform slot;
    List<InventoryUISlot> slots = new List<InventoryUISlot>();
  
    [Header("Prefabs")]
    [SerializeField]
    GameObject wordItemPrefab;

    [Header("Inventory Stats")]
    [SerializeField]
    [Range(1, 15)]
    int inventorySpace;
    [SerializeField]
    int currentWealth;

    GameObject view;

    private void OnEnable()
    {
        WorldItem.OnItemPickUp += Add;
        PlayerController.OnInventoryUIToggle += ToggleView;
        ShopSlot.OnItemBought += AddBoughtItem;
        ShopSlot.OnItemSold += RemoveSoldItem;
    }

    private void OnDisable()
    {
        WorldItem.OnItemPickUp -= Add;
        PlayerController.OnInventoryUIToggle -= ToggleView;
        ShopSlot.OnItemBought -= AddBoughtItem;
        ShopSlot.OnItemSold -= RemoveSoldItem;
    }
    private void Start()
    {
        view = transform.Find("View").gameObject;
        Init(inventorySpace);
    }
    public void ToggleView()
    {
        view.SetActive(!view.activeSelf);
    }
    #region Add & Remove
    public bool Add(Item newItem)
    {
        if(newItem is Coin)
        {
            SetCurrentWealth(GetCurrentWealth()+newItem.value);
            return true;
        }

        for (int i = 0; i < slots.Count; i++) //Checking every single slot for a duplicate, even the empty ones.
        {
            if (slots[i].GetItem() != null && slots[i].GetItem().id == newItem.id) //Check matching ID
            {
                if (slots[i].GetStack()+1 <= newItem.stackLimit) // If ID matches. Check stack limit.
                {
                    slots[i].SetItem(newItem);
                    return true;
                }
            }
        }
        for (int i = 0; i < slots.Count; i++) //Since there were no duplicates, fill the first empty slot you find.
        {
            if (slots[i].GetItem() == null) //is the slot empty?
            {
                slots[i].SetItem(newItem); //fill the slot.
                return true;
            }
        }
        return false;
    }

    void AddBoughtItem(Item newItem)
    {
        SetCurrentWealth(GetCurrentWealth() - newItem.value);
        Add(newItem);
    }


    public void Remove(Item itemToRemove)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetItem().id == itemToRemove.id) //Tengo un item con este ID?
            {
                if (slots[i].GetStack() > 0) // Ese item tiene por lo menos un objeto en el stack?
                {
                    slots[i].SetStack(slots[i].GetStack()-1);
                }
                else
                {
                    slots[i].SetItem(null);
                }
                break; //Salgo del for al primero que encuentre.
            }
        }
    }
    
    void RemoveSoldItem(Item newItem)
    {
        SetCurrentWealth(GetCurrentWealth() + newItem.value);
        Remove(newItem);
    }

    #endregion


    public void CheckOwnership(Item item, GameObject interactor)
    {

    }


    #region Init and Refresh
    public void Init(int inventorySpace)
    {
        for (int i = 0; i < maxRows; i++)
        {
            for (int j = 0; j < maxColumns; j++)
            {
                if (slots.Count < inventorySpace)
                {
                    RectTransform slotInstance = Instantiate(slot, container).GetComponent<RectTransform>();
                    slotInstance.gameObject.SetActive(true);
                    slotInstance.gameObject.name = "Inventory Slot x:" + j + " y:"+i;
                    slotInstance.anchoredPosition = new Vector2(j * cellSizeX, -i * cellSizeY);
                    slots.Add(slotInstance.gameObject.GetComponent<InventoryUISlot>());
                }
                else
                {
                    break;
                }
            }
        }
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetItem(slots[i].GetItem());
        }
        OnWealthUpdate?.Invoke(currentWealth);
    }
    #endregion


    public int GetCurrentWealth()
    {
        return currentWealth;
    }

    public void SetCurrentWealth(int newWealth)
    {
        currentWealth = newWealth;
        OnWealthUpdate?.Invoke(currentWealth); //Es poco ortodoxo, pero si estoy llamando a SetCurrentWealth siempre voy a querer updatear lo pertinente.
    }

}
