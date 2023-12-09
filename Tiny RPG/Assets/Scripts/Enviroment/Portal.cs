using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    Animator animator;
    [SerializeField]
    string destination;
    [SerializeField]
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ShowPrompt()
    {
        canvas.gameObject.SetActive(true);
    }
    public void Interact(GameObject interactor)
    {
        SceneManager.LoadScene(destination);
    }
    public void HidePrompt()
    {
        canvas.gameObject.SetActive(false);
    }
}
