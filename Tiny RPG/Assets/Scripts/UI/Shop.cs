using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    List<Item>  items = new List<Item>();
    List<RectTransform> slots = new List<RectTransform>();

    [SerializeField]
    RectTransform container;
    [SerializeField]
    RectTransform slot;
    [SerializeField]
    int totalSlots;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < totalSlots; i++)
        {
            RectTransform slotInstance = Instantiate(slot, container).GetComponent<RectTransform>();
            slotInstance.gameObject.SetActive(true);
            slots.Add(slotInstance);
            if (i < items.Count )
            {
                if(items[i] != null)
                {
                    slots[i].GetComponent<ShopSlot>().SetItem(items[i]);
                }
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}
