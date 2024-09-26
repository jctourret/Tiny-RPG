using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class InventoryUISlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,
IDragHandler,IBeginDragHandler, IEndDragHandler, IDropHandler
{ 
    protected enum UIElements
    {
        slot,
        background,
        item
    }

    public static Action<Item> OnItemUsed;

    protected Canvas canvas;

    [SerializeField]
    protected Item itemInSlot;
    public int stack;

    [SerializeField]
    Sprite pressedSprite;
    [SerializeField]
    Sprite unpressedSprite;

    [SerializeField]
    protected RectTransform[] rectTransforms;
    [SerializeField]
    protected Image[] images;

    TextMeshProUGUI textmesh;

    protected Vector3 itemPosition;

    float verChange = -8.8f;
    float verBgChange = -5f;

    protected float holdTimer = 0;
    protected float holdTime = 0.2f;

    protected bool pointerDown = false;
    protected bool canDrag = false;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransforms = GetComponentsInChildren<RectTransform>();
        textmesh = GetComponentInChildren<TextMeshProUGUI>(true);
        images = GetComponentsInChildren<Image>();
        canvas = GetComponentInParent<Canvas>();
        itemPosition = rectTransforms[(int)UIElements.item].localPosition;
    }

    protected void Update()
    {
        if (pointerDown)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTime)
            {
                Debug.Log("Holding button");
                holdTimer = 0;

                canDrag = true;

                pointerDown = false;
                holdTimer = 0;
            }
        }
    }
    #region Click
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        PressButton();
        pointerDown = true;
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        UnpressButton();
        Debug.Log("inv pointer Up");
        if (holdTimer < holdTime && itemInSlot != null)
        {
            OnItemUsed?.Invoke(itemInSlot);
        }
        pointerDown = false;
        holdTimer = 0;
    }
    void PressButton()
    {
        if (itemInSlot != null)
        {
            images[(int)UIElements.slot].sprite = pressedSprite;
            rectTransforms[(int)UIElements.slot].position += new Vector3(0, verChange, 0);
            rectTransforms[(int)UIElements.slot].sizeDelta += new Vector2(0, verChange);
            rectTransforms[(int)UIElements.background].position += new Vector3(0, verBgChange, 0);
            foreach (Image image in images)
            {
                image.color = ChangeColorValue(image.color, 0.9f);
            }
        }
    }
    protected void UnpressButton()
    {
        if (itemInSlot != null)
        {
            images[(int)UIElements.slot].sprite = unpressedSprite;

            rectTransforms[(int)UIElements.slot].position -= new Vector3(0, verChange, 0);
            rectTransforms[(int)UIElements.slot].sizeDelta -= new Vector2(0, verChange);
            rectTransforms[(int)UIElements.background].position -= new Vector3(0, verBgChange, 0);
            foreach (Image image in images)
            {
                image.color = ChangeColorValue(image.color, 1f);
            }
        }
    }

    public Color ChangeColorValue(Color color, float newValue)
    {
        // Convert color to HSV
        Color.RGBToHSV(color, out float h, out float s, out float v);

        // Modify the value (brightness)
        v = Mathf.Clamp01(newValue);

        // Convert back to RGB
        Color modifiedColor = Color.HSVToRGB(h, s, v);

        // Apply the modified color
        return modifiedColor;
    }
    #endregion

    #region Drag & Drop
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        rectTransforms[((int)UIElements.item)].SetParent(rectTransforms[((int)UIElements.slot)].parent.transform.parent);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (canDrag && Input.GetMouseButton((int)MouseButton.Left))
        {
            rectTransforms[((int)UIElements.item)].position = eventData.position/canvas.scaleFactor;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canDrag = false;
        rectTransforms[((int)UIElements.item)].SetParent(rectTransforms[(int)UIElements.background],false);
        rectTransforms[((int)UIElements.item)].localPosition = itemPosition;
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        Debug.Log( eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        InventoryUISlot droppedSlot = eventData.pointerDrag.GetComponent<InventoryUISlot>();
        if(droppedSlot != null)
        {
            if(itemInSlot == null)
            {
                if(droppedSlot is EquipmentSlot)
                {
                    EquipmentSlot equipSlot = droppedSlot as EquipmentSlot;
                    SetItem(droppedSlot.itemInSlot);
                    equipSlot.SetItem(null);
                }
                else if (droppedSlot is ShopSlot)
                {
                    ShopSlot shopSlot = droppedSlot as ShopSlot;
                    ShopSlot.OnItemBought?.Invoke(shopSlot.GetItem());
                }
                else
                {
                    stack = droppedSlot.stack;
                    SetItem(droppedSlot.itemInSlot);
                    droppedSlot.SetItem(null);
                    droppedSlot.stack = 0;
                }
                Debug.Log("item passed");
            }
            else
            {
                Debug.Log(gameObject.name + " already has an item");
            }
        }
    }
    #endregion

    #region Getters & Setters

    public Item GetItem()
    {
        return itemInSlot;
    }
    public virtual void SetItem(Item newItem)
    {
        if(newItem != null) //Was I given a non null item?
        {
            if(itemInSlot != newItem) //Is it a different item I have?
            {
                itemInSlot = newItem;
                images[(int)UIElements.item].sprite = itemInSlot.sprite;
                images[(int)UIElements.item].color = Color.white;
                stack = 0; //Reset the numbers of items stacked.
            }
            stack++; //I have one more of this item
            if (stack > 1) //only write the number if there is more than one. 
            {
                textmesh.text = stack.ToString();
            }
            else
            {
                textmesh.text = "";
            }
        }
        else
        {
            itemInSlot = null;
            images[(int)UIElements.item].color = Color.clear;
            textmesh.text = "";
        }
    }
    public int GetStack()
    {
        return stack;
    }
    public void SetStack(int newStack)
    {
        stack = newStack;
    }
#endregion
}
