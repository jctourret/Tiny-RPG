using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{


    void Start()
    {
        
    }

    void Update()
    {

    }

    public void TakeDamage()
    {
        Destroy(this);
    }

}
