using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void ShowPrompt();
    public void Interact(GameObject interactor);
    public void HidePrompt();
}
