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

    public void EnterInteractionRange()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Interact()
    {
        Destroy(gameObject);
    }
    public void ExitInteractionRange()
    {
        canvas.gameObject.SetActive(false);
    }


}
