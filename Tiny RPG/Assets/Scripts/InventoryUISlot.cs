using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUISlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Item itemInSlot;

    [SerializeField]
    Sprite pressedSprite;
    [SerializeField]
    Sprite unpressedSprite;

    RectTransform rectTransform;
    [SerializeField]
    RectTransform bgTransform;

    Image image;
    [SerializeField]
    Image bgImage;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TextMeshProUGUI textmesh;

    float verChange = -8.8f;
    float verBgChange = -5f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        textmesh = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = unpressedSprite;
        rectTransform.position -= new Vector3(0, verChange, 0);
        rectTransform.sizeDelta -= new Vector2(0, verChange);
        bgTransform.position -= new Vector3(0, verBgChange, 0);
        image.color = ChangeColorValue(image.color, 1f);
        bgImage.color = ChangeColorValue(bgImage.color,1f);
        itemImage.color = ChangeColorValue(itemImage.color,1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = pressedSprite;
        rectTransform.position += new Vector3(0, verChange, 0);
        rectTransform.sizeDelta += new Vector2(0, verChange);
        bgTransform.position += new Vector3(0, verBgChange, 0);
        image.color = ChangeColorValue(image.color, 0.9f);
        bgImage.color = ChangeColorValue(bgImage.color, 0.9f);
        itemImage.color = ChangeColorValue(itemImage.color, 0.9f);
    }

    public Color ChangeColorValue(Color color,float newValue)
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

    public void SetItem(Item newItem)
    {
        if(newItem != null)
        {
            itemInSlot = newItem;
            itemImage.sprite = itemInSlot.sprite;
            itemImage.color = Color.white;
            if (itemInSlot.stack > 0)
            {
                textmesh.text = itemInSlot.stack.ToString();
            }
            else
            {
                textmesh.text = "";
            }
        }
        else
        {
            itemImage.color = Color.clear;
            textmesh.text = "";
        }
    }

}
