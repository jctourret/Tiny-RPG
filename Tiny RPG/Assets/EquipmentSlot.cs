using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Linq;

public class EquipmentSlot : InventoryUISlot, IPointerUpHandler, IPointerDownHandler,
IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public static Action<Equipment> OnItemEquipped;
    public Equipment.EquipmentSlots slot;

    public new void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        images[(int)UIElements.background].enabled = true;
    }
    public new void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }
    public new void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        InventoryUISlot droppedSlot = eventData.pointerDrag.GetComponent<InventoryUISlot>();
        if (droppedSlot != null)
        {
            if(droppedSlot.GetItem() is Equipment)
            {
                Equipment itemToEquip = droppedSlot.GetItem() as Equipment;
                if (itemToEquip.slot == this.slot)
                {
                    OnItemEquipped?.Invoke(itemToEquip);
                    droppedSlot.SetItem(null);
                    this.SetItem(itemToEquip);
                }
            }
        }
    }
    public override void SetItem(Item newItem)
    {
        if (newItem != null)
        {
            itemInSlot = newItem;
            images[(int)UIElements.item].enabled = true;
            images[(int)UIElements.background].enabled = false;
            images[(int)UIElements.item].sprite = itemInSlot.sprite;
        }
        else
        {
            itemInSlot = null;
            images[(int)UIElements.item].enabled = false;
        }
    }
}
