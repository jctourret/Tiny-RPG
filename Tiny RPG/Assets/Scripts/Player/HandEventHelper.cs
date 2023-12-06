using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEventHelper : MonoBehaviour
{
    [SerializeField]
    Hand hand;
    public void StartAttack()
    {
        hand.StartAttack();
    }
    public void EndAttack()
    {
        hand.EndAttack();
    }
}
