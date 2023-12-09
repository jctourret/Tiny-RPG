using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    public static Func<Item, GameObject, bool> OnItemPickUp;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Item currentItem;
    new SpriteRenderer renderer;
    private void Awake()
    {
        gameObject.name = currentItem.name;
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = currentItem.sprite;
    }
    public void ShowPrompt()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Interact(GameObject interactor)
    {
        PickUpItem(interactor);
    }
    public void HidePrompt()
    {
        canvas.gameObject.SetActive(false);
    }
    public void PickUpItem(GameObject interactor)
    {
        Debug.Log("Picking up" + currentItem.name);
        if (OnItemPickUp?.Invoke(currentItem, interactor) == true)
        {
            Destroy(gameObject);
        }
    }
}
