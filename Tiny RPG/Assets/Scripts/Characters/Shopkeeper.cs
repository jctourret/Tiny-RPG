using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject prompt;

    [SerializeField]
    GameObject shop;
    private void Awake()
    {
    }
    public void ExitInteractionRange()
    {
        prompt.SetActive(false);
        shop.SetActive(false);
    }

    public void Interact()
    {
        shop.SetActive(!shop.activeSelf);
    }

    public void EnterInteractionRange()
    {
        prompt.SetActive(true);
    }
}
