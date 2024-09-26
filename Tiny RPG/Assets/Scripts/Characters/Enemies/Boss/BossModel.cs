using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BossModel : CharacterStats
{
    public enum States
    {
        Idle,
        Charge,
        Stunned,
        Laser,
        Slam,
        Jump
    }

    [SerializeField]
    int attack;

    public float knockbackRecoveryTime = 2;
    public float selfKnockbackForce = 3;
    // Start is called before the first frame update
    [Header("State")]
    public States previousState;
    public States currentState;
    public bool enterState;
    public bool onState;
    public bool endState;

    [Header("Idle")]
    public float idleTime;
    public float idleTimer;

    [Header("Charge")]
    public float chargeSpeed;
    public float chargeAccel;
    public float chargeTimer;
    public float chargeDelay;
    public Vector3 chargeStartPos;
    public float chargeMaxDistance;
    float distanceFromStart;
    [Header("Laser")]
    public Vector3 laserDir;
    public Vector3 laserEnd;
    public float laserAngle;
    public float laserSpeed;
    public float laserWidth;
    public float laserDuration;
    public float laserTimer;

    private void Start()
    {
        enterState = true;
    }

    public bool RunTimer(float timer, float time)
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            timer = 0;
            return true;
        }
        return false;
    }

    public int GetAttack()
    {
        return attack;
    }

    public void SetChargeStartPos(Vector3 pos)
    {
        chargeStartPos = pos;
    }

    public Vector3 GetChargeStartPos()
    {
        return chargeStartPos;
    }

    public void SetDistanceFromStart(float distance)
    {
        distanceFromStart = distance;
    }

    public float GetDistanceFromStart()
    {
        return distanceFromStart;
    }
}
