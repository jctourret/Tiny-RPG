using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSlot : InventoryUISlot
{

    public static Action<Item> OnItemBought;
    public static Action<Item> OnItemSold;

    public override void OnPointerUp(PointerEventData eventData)
    {
        UnpressButton();
        Debug.Log("Shop pointer Up");
        if (holdTimer < holdTime && itemInSlot != null)
        {
            OnItemBought?.Invoke(itemInSlot);
        }
        pointerDown = false;
        holdTimer = 0;
    }
    private new void Update()
    {
        base.Update();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        //rectTransforms[((int)UIElements.item)].SetParent(rectTransforms[(int)UIElements.slot].parent.parent);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (canDrag && Input.GetMouseButton((int)MouseButton.Left))
        {
            rectTransforms[((int)UIElements.item)].position = eventData.position / canvas.scaleFactor;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        canDrag = false;
        rectTransforms[((int)UIElements.item)].SetParent(rectTransforms[(int)UIElements.background], false);
        rectTransforms[((int)UIElements.item)].localPosition = itemPosition;

    }

    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        InventoryUISlot droppedSlot = eventData.pointerDrag.GetComponent<InventoryUISlot>();
        if (droppedSlot != null)
        {
            if (droppedSlot.GetItem() is Item)
            {
                OnItemSold?.Invoke(itemInSlot);
            }
        }
    }



}
