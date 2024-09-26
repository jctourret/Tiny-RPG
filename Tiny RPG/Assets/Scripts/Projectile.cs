using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    int damage;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up* speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && !collision.isTrigger)
        {
            Destroy(gameObject);
            IDamageable damageable = collision.GetComponentInChildren<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage, Vector3.zero);
            }
        }
    }

}
