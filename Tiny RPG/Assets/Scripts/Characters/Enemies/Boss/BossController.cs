using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class BossController : MonoBehaviour
{

    BossModel model;
    BossView view;

    Rigidbody2D body;

    [SerializeField]
    Transform target;

    IAstarAI starAI;

    [Header("ChargeDebug")]
    [Header("Impact Direction")]
    [SerializeField]
    Vector2 impactDir;

    [Header("Point of impact")]
    [SerializeField]
    Vector2 gizmoDrawpoint;
    float gizmoRadius = 0.1f;


    [SerializeField]
    float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponentInChildren<BossModel>();
        view = GetComponentInChildren<BossView>();
        body = GetComponent<Rigidbody2D>(); 
        starAI = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        RunState();
    }

    public void RunState()
    {
        switch (model.currentState)
        {
            case BossModel.States.Idle:
                RunIdle();
                break;
            case BossModel.States.Charge:
                RunCharge();
                break;
            case BossModel.States.Laser:
                RunLaser();
                break;
            case BossModel.States.Slam:
                RunIdle();
                break;
            case BossModel.States.Jump:
                RunIdle();
                break;
            default:
                break;
        }
    }
    public void RunIdle()
    {
        if (model.enterState)
        {
            //Code on state entry
            model.enterState = false;
            model.onState = true;
        }
        else if (model.onState)
        {
            //Eventualmente mudar a funcion "OnIdleState()"
            model.idleTimer += Time.deltaTime;
            if (model.idleTimer >= model.idleTime)
            {
                switch (model.previousState)
                {
                    case BossModel.States.Idle:
                        model.previousState = model.currentState;
                        model.currentState = BossModel.States.Charge;
                        break;
                    case BossModel.States.Charge:
                        model.previousState = model.currentState;
                        model.currentState = BossModel.States.Laser;
                        break;
                    case BossModel.States.Laser:
                        model.previousState = model.currentState;
                        model.currentState = BossModel.States.Charge;
                        break;
                    case BossModel.States.Slam:
                        break;
                    case BossModel.States.Jump:
                        break;
                }
                model.idleTimer = 0;
                //Salgo del estado actual y empiezo en el enter del siguiente.
                model.onState = false;
                model.enterState = true;
            }
        }
        else if (model.endState)
        {
            //Code on state exit
            model.endState = false;
            model.enterState = true;
        }
    }

    public void RunCharge()
    {
        if (model.enterState)
        {
            //Eventualmente mudar a funcion "EnterChargeState()"
            //Seteo donde empeze la carga.
            model.SetChargeStartPos(transform.position);
            //Seteo la direccion a mi objetivo.
            Vector2 rayDir = target.position - transform.position;
            //Disparo un rayo que me da el punto al que quiero ir.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir,float.PositiveInfinity,
                LayerMask.GetMask("Obstacle") & ~LayerMask.GetMask("Player"));

            starAI.canMove = true;
            starAI.destination = hit.point;
            starAI.SearchPath();

            model.enterState = false;
            model.onState = true;
        }
        else if (model.onState)
        {
            //Actualizo la distancia recorrida
            model.SetDistanceFromStart(Vector3.Distance(model.GetChargeStartPos(),transform.position));

            //si mi distancia recorrida es mayor a mi distancia maxima, me detengo.
            if (model.GetDistanceFromStart() >= model.chargeMaxDistance)
            {
                model.SetDistanceFromStart(0);
                model.previousState = model.currentState;
                model.currentState = BossModel.States.Idle;

                model.onState = false;
                model.endState = true;
            }
        }
        else if (model.endState)
        {
            model.endState = false;
            model.enterState = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I hit" + collision.collider.tag);
        IDamageable damageable = collision.gameObject.GetComponentInChildren<IDamageable>();
        if (damageable != null)
        {
            Vector3 knockbackDirection = collision.gameObject.transform.position - transform.position;
            knockbackDirection.Normalize();

            Vector3 selfKnockback = -knockbackDirection * model.selfKnockbackForce;
            Vector3 attackKnockback = knockbackDirection * model.GetKnockback();

            damageable.TakeDamage(model.GetAttack(), attackKnockback);
            Debug.Log("Spider damaged " + collision.gameObject.name);

            StartKnockback(selfKnockback);
        }
        //Si estoy cargando y le pego a un obstaculo, me stuneo
        if(model.currentState == BossModel.States.Charge &&
            collision.collider.CompareTag("Obstacle"))
        {
            Vector3 knockbackDirection = collision.GetContact(0).point - (Vector2)transform.position;
            gizmoDrawpoint = collision.GetContact(0).point;
            knockbackDirection.Normalize();
            impactDir = knockbackDirection;

            Vector3 selfKnockback = -knockbackDirection*model.selfKnockbackForce;


            StartKnockback(selfKnockback);

            model.enterState = true;
            model.onState = false;
            model.endState = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        if (impactDir != Vector2.zero)
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)impactDir);
            Gizmos.DrawRay(transform.position, impactDir);
        }
        Gizmos.DrawSphere(gizmoDrawpoint,gizmoRadius);
    }

    public void StartKnockback(Vector3 knockback)
    {
        StartCoroutine(Knockback(starAI, knockback));
        Debug.Log("Boss knocked back after hiting");
    }
    public IEnumerator Knockback(IAstarAI ai, Vector3 knockbackDir)
    {
        // Optionally make the AI stop trying to move while being pushed
        ai.isStopped = true;
        for (float t = 0; t < model.knockbackRecoveryTime; t += Time.deltaTime)
        {
            ai.Move(knockbackDir * Time.deltaTime * 1 / (t + 0.1f));
            yield return null;
        }
        // Optional
        ai.isStopped = false;
        model.previousState = model.currentState;
        model.currentState = BossModel.States.Idle;
        ai.destination = transform.position;
        body.velocity = Vector2.zero;
        //ai.canSearch = false;

        Debug.Log("Boss in no longer knocked back");
    }


    public void RunLaser()
    {
        if (model.enterState)
        {
            //Eventualmente mudar a funcion "EnterChargeState()"
            //Me quedo quieto para disparar el laser.
            starAI.canMove = false;
            //Seteo la direccion a mi objetivo.
            model.laserDir = target.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, model.laserDir, float.PositiveInfinity,
                LayerMask.GetMask("Obstacle") & ~LayerMask.GetMask("Player"));

            model.laserAngle = 0;


            view.EnableLineRenderer();
            view.UpdateLineRenderer(model.laserWidth,transform.position,hit.point);

            model.enterState = false;
            model.onState = true;
        }
        else if (model.onState)
        {
            if (model.RunTimer(model.laserTimer, model.laserDuration))
            {
                model.onState = false;
                model.endState = true;
            }
            else
            {
                //Calculo cuanto me tengo que mover este frame
                float angleThisFrame = model.laserSpeed * Time.deltaTime;
                //Calculo la direccion objetivo
                Vector3 targetDir = target.position - transform.position;
                targetDir.Normalize();
                //Usando la direccion de mi laser y la direccion de mi objetivo
                //consigo el angulo que necesito alcanzar.
                targetAngle = (Vector2.SignedAngle((Vector2)model.laserDir, targetDir))*2;

                //Si el angulo es positivo, significa que tengo que girar en una direccion
                //si es negativo, tengo que girar en otra.
                if (targetAngle > 0)
                {
                    model.laserAngle += angleThisFrame;
                }
                else if (targetAngle < 0)
                {
                    model.laserAngle -= angleThisFrame;
                }
                //paso mi angulo a radianes y calculo la nueva direccion de mi laser.
                float angleInRadians = model.laserAngle * Mathf.Deg2Rad;
                model.laserDir = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
                //disparo mi laser
                RaycastHit2D hit = Physics2D.Raycast(transform.position, model.laserDir, float.PositiveInfinity, LayerMask.GetMask("Obstacle"));
                //actualizo el line renderer.
                view.UpdateLineRenderer(model.laserWidth,transform.position, hit.point);
            }
        }
        else if (model.endState)
        {
            starAI.canMove = true;
            //Code on state exit
            model.endState = false;
            model.enterState = true;
        }
    }
    public void RunJump()
    {
        if (model.enterState)
        {
            //Code on state entry
            model.enterState = false;
            model.onState = true;
        }
        else if (model.onState)
        {
            model.onState = false;
            model.endState = true;
        }
        else if (model.endState)
        {
            //Code on state exit
            model.endState = false;
            model.enterState = true;
        }

    }





    /*Default function structure.
    void ExampleState()
    {
        if (model.enterState)
        {
            //Code on state entry
            model.enterState = false;
            model.onState = true;
        }
        else if (model.onState)
        {
            model.onState = false;
            model.endState = true;
        }
        else if (model.endState)
        {
            //Code on state exit
            model.endState = false;
            model.enterState = true;
        }
    }
    */
}
