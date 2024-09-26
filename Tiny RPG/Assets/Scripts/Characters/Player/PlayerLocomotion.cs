using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField]
    float speed = 10000;
    [SerializeField]
    float sprintSpeed = 5000;


    SpriteRenderer spriteRenderer;
    Vector2 direction;
    [SerializeField]
    Vector2 velocity;
    bool isSprinting;

    Animator animator;
    Rigidbody2D rb;
    float linearDrag = 30;
    Camera cam;

    private void OnEnable()
    {
        PlayerModel.OnPlayerHit += Knockback;
    }

    private void OnDisable()
    {
        PlayerModel.OnPlayerHit += Knockback;
    }

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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        cam = Camera.main;
    }
    private void Update()
    {
        animator.SetFloat("MoveMag",rb.velocity.sqrMagnitude);
        animator.SetBool("IsSprinting",isSprinting);
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimdirection = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg;
        if(angle < 90 && angle > -90)
        {
            transform.localScale = new Vector3(1,transform.localScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(direction * (speed + (sprintSpeed * ( isSprinting ? 1 : 0))), ForceMode2D.Force);
        velocity = rb.velocity;
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

    public void Knockback(int damage, Vector3 knockback)
    {
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
