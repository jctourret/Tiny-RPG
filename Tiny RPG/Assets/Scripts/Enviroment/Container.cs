using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Sprite unpressed;
    [SerializeField]
    Sprite pressed;

    public void ShowPrompt()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Interact(GameObject interactor)
    {
        Destroy(gameObject);
    }
    public void HidePrompt()
    {
        canvas.gameObject.SetActive(false);
    }


}
