using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour
{
    Hand hand;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        hand  = GetComponentInParent <Hand>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void StartAttack()
    {
        hand.StartAttack();
    }
    public void EndAttack()
    {
        hand.EndAttack();
    }
}
