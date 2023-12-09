using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    const int maxColumns = 5;
    const int maxRows = 5;
    [SerializeField]
    float cellSize = 120f;

    [SerializeField]
    RectTransform container;
    [SerializeField]
    RectTransform slot;
    List<InventoryUISlot> slots = new List<InventoryUISlot>();
    GameObject view;

    private void OnEnable()
    {
        PlayerInventory.OnPlayerInvUpdate += Refresh;
        PlayerInventory.OnPlayerInvInit += Init;
        PlayerInventory.OnInventoryUIToggle += ToggleView;
    }

    private void OnDisable()
    {
        PlayerInventory.OnPlayerInvUpdate -= Refresh;
        PlayerInventory.OnPlayerInvInit -= Init;
        PlayerInventory.OnInventoryUIToggle -= ToggleView;
    }
    private void Start()
    {
        view = transform.Find("View").gameObject;
    }
    public void ToggleView()
    {
        view.SetActive(!view.activeSelf);
    }
    public void Init(List<Item> items, int inventorySpace)
    {
        for (int i = 0; i < maxColumns; i++)
        {
            for (int j = 0; j < maxRows; j++)
            {
                if (slots.Count < inventorySpace)
                {
                    RectTransform slotInstance = Instantiate(slot, container).GetComponent<RectTransform>();
                    slotInstance.gameObject.SetActive(true);
                    slotInstance.anchoredPosition = new Vector2(j * cellSize, -i * cellSize);
                    slots.Add(slotInstance.gameObject.GetComponent<InventoryUISlot>());
                }
                else
                {
                    break;
                }
            }
        }
        Refresh(items);
    }

    public void Refresh(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].SetItem(items[i]);
        }
        for (int i = items.Count; i < slots.Count; i++)
        {
            slots[i].SetItem(null);
        }
    }
}
