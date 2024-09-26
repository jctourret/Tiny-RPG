using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField]
    SceneList.Scenes destination;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponentInChildren<Canvas>();
    }
    public void EnterInteractionRange()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Interact()
    {
        SceneManager.LoadScene((int)destination);
    }
    public void ExitInteractionRange()
    {
        canvas.gameObject.SetActive(false);
    }
}
