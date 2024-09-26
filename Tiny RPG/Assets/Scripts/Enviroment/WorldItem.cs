using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    public static Func<Item, bool> OnItemPickUp;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Item currentItem;
    new SpriteRenderer renderer;
    private void Awake()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateItem();
    }
    public void EnterInteractionRange()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Interact()
    {
        PickUpItem();
    }
    public void ExitInteractionRange()
    {
        canvas.gameObject.SetActive(false);
    }
    public void PickUpItem()
    {
        Debug.Log("Picking up" + currentItem.name);
        if (OnItemPickUp?.Invoke(currentItem) == true)
        {
            Destroy(gameObject);
        }
    }
    public void SetItem(Item newItem)
    {
        currentItem = newItem;
        UpdateItem();
    }

    public Item GetItem()
    {
        return currentItem;
    }

    public void UpdateItem()
    {
        gameObject.name = currentItem.name;
        renderer.sprite = currentItem.sprite;
    }
}
