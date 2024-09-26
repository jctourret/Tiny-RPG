using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void EnterInteractionRange();
    public void Interact();
    public void ExitInteractionRange();
}
