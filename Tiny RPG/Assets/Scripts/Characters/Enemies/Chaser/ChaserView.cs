using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserView : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetMoveMag(float movemag)
    {
        animator.SetFloat("MoveMag",movemag);
    }
    public void IsHurt()
    {
        animator.SetTrigger("IsHurt");
    }
}
