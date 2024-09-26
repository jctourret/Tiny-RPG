using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Util;
using Pathfinding;

public class ChaserController : Character
{
    [SerializeField]
    float knockbackRecoveryTime = 2;
    [SerializeField]
    float selfKnockbackForce = 3;
    IAstarAI starAI;
    ChaserModel model;
    ChaserView view;
    Rigidbody2D rb2;

    Vector2 lastPos;
    void Start()
    {
        model = GetComponentInChildren<ChaserModel>();
        starAI = GetComponent<IAstarAI>();
        rb2 = GetComponent<Rigidbody2D>();
        view = GetComponentInChildren<ChaserView>();
        lastPos = transform.position;
    }
    private void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        float movemag = (lastPos - currentPos).magnitude;
        view.SetMoveMag(movemag);
        lastPos = currentPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponentInChildren<IDamageable>();
        ChaserController enemyController = collision.gameObject.GetComponent<ChaserController>();
        if(damageable != null && enemyController == null)
        {

            Vector3 knockbackDirection =  collision.gameObject.transform.position - transform.position;
            knockbackDirection.Normalize();

            Vector3 selfKnockback = -knockbackDirection * selfKnockbackForce;
            Vector3 attackKnockback = knockbackDirection * model.GetKnockback();

            damageable.TakeDamage(model.GetAttack(),attackKnockback);
            Debug.Log("Spider damaged " + collision.gameObject.name);

            StartKnockback(selfKnockback);
        }
    }

    public void IsHurt()
    {
        view.IsHurt();
    }

    public void StartKnockback(Vector3 knockback)
    {
        StartCoroutine(Knockback(starAI, knockback));
        Debug.Log("Spider knocked back after hiting");
    }
    public IEnumerator Knockback(IAstarAI ai, Vector3 knockbackDir)
    {
        // Optionally make the AI stop trying to move while being pushed
        //ai.isStopped = true;
        for (float t = 0; t < knockbackRecoveryTime; t += Time.deltaTime)
        {
            ai.Move(knockbackDir * Time.deltaTime * 1 / (t + 0.1f));
            yield return null;
        }
        // Optional
        //ai.isStopped = false;
    }
}
