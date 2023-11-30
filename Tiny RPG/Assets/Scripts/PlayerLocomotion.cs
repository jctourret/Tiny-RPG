using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField]
    float speed = 10000;
    [SerializeField]
    float sprintSpeed = 5000;

    Vector2 direction;
    bool isSprinting;

    Animator animator;
    Rigidbody2D rb;
    float linearDrag = 30;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (!(rb = GetComponent<Rigidbody2D>()))
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = linearDrag;
        }
    }
    private void Update()
    {
        animator.SetFloat("MoveMag",rb.velocity.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        rb.AddForce(direction * (speed + (sprintSpeed * ( isSprinting ? 1 : 0))) * Time.deltaTime, ForceMode2D.Force);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        direction.Normalize();
    }

    public void SetSprinting(bool sprinting)
    {
        this.isSprinting = sprinting;

    }
}
