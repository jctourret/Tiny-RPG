using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerLocomotion locomotion;

    // Start is called before the first frame update
    void Start()
    {
        locomotion = GetComponent<PlayerLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        AttackInput();
    }
    void MoveInput()
    {
        locomotion.SetDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        locomotion.SetSprinting(Input.GetButtonDown("Sprint"));
    }
    void AttackInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }
    }
}
