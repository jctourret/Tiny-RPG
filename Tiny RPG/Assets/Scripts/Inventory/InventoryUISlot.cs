using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUISlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler,
IDragHandler,IBeginDragHandler, IEndDragHandler, IDropHandler
{ 
    enum UIElements
    {
        slot,
        background,
        item
    }

    public static Action<Item> OnItemUsed;

    Canvas canvas;

    [SerializeField]
    Item itemInSlot;
    public int stack;

    [SerializeField]
    Sprite pressedSprite;
    [SerializeField]
    Sprite unpressedSprite;

    [SerializeField]
    RectTransform[] rectTransforms;
    [SerializeField]
    Image[] images;

    TextMeshProUGUI textmesh;

    Vector3 itemPosition;

    float verChange = -8.8f;
    float verBgChange = -5f;

    float holdTimer = 0;
    float holdTime = 0.2f;

    bool pointerDown = false;
    bool canDrag = false;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransforms = GetComponentsInChildren<RectTransform>();
        textmesh = GetComponentInChildren<TextMeshProUGUI>(true);
        images = GetComponentsInChildren<Image>();
        canvas = GetComponentInParent<Canvas>();
        itemPosition = rectTransforms[(int)UIElements.item].localPosition;
    }

    #region Click
    public void OnPointerDown(PointerEventData eventData)
    {
        PressButton();
        pointerDown = true;
    }
    private void Update()
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
    public void OnPointerUp(PointerEventData eventData)
    {
        UnpressButton();
        if(holdTimer< holdTime)
        {
            itemInSlot.Use();
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

    void UnpressButton()
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransforms[((int)UIElements.item)].SetParent(rectTransforms[((int)UIElements.slot)].parent);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            rectTransforms[((int)UIElements.item)].position = eventData.position/canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canDrag = false;
        rectTransforms[((int)UIElements.item)].SetParent(rectTransforms[(int)UIElements.background],false);
        rectTransforms[((int)UIElements.item)].localPosition = itemPosition;

    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log( eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        InventoryUISlot droppedSlot = eventData.pointerDrag.GetComponent<InventoryUISlot>();
        if(droppedSlot != null)
        {
            if(itemInSlot == null)
            {
                SetItem(droppedSlot.itemInSlot);
                droppedSlot.SetItem(null);
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
    public void SetItem(Item newItem)
    {
        if(newItem != null)
        {
            if(itemInSlot == newItem)
            {
                stack++;
            }
            else
            {
                itemInSlot = newItem;
                images[(int)UIElements.item].sprite = itemInSlot.sprite;
                images[(int)UIElements.item].color = Color.white;
            }
            if (stack > 1)
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
