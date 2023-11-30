using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField]
    float speed = 100;
    Vector2 direction;
    
    Rigidbody2D rb;
    float linearDrag = 30;
    // Start is called before the first frame update
    void Start()
    {
        if (!(rb = GetComponent<Rigidbody2D>()))
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = linearDrag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Force);
    }
}
