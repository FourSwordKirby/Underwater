using UnityEngine;
using System.Collections;

/// <summary>
/// Squids should float. When the player enter's their range, they should charge up and then spin towards the player
/// </summary>
public class SquidAttackState : State<SquidEnemy>
{
    private SquidEnemy enemy;


    private float chargeLength = 5.0f;
    private float attackLength = 0.5f;
    private float recoveryLength = 1.5f;
    private float timer;

    private float swimSpeed = 10.0f;

    private attackPhase phase;

    private enum attackPhase
    {
        Charging,
        Attacking,
        Recovering
    }

    public SquidAttackState(SquidEnemy enemyInstance, StateMachine<SquidEnemy> fsm, GameObject currentTarget)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
        enemy.currentTarget = currentTarget;
    }

    override public void Enter()
    {
        enemy.selfBody.gravityScale = 0.0f;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
    }

    override public void FixedExecute()
    {
        timer += Time.deltaTime;
        //prepping to attack player
        if (phase == attackPhase.Charging)
        {
            enemy.selfBody.velocity = Vector2.Lerp(enemy.selfBody.velocity, Vector2.zero, Time.deltaTime * chargeLength / 2);

            Vector3 dir = enemy.currentTarget.transform.position - enemy.transform.position;
            float angle = Vector2.Angle(Vector2.up, dir);
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, targetRotation, 60 * Time.deltaTime);

            if (timer > chargeLength)
            {
                timer = 0.0f;
                phase = attackPhase.Attacking;
            }
        }
        if (phase == attackPhase.Attacking)
        {
            Vector3 dir = enemy.currentTarget.transform.position - enemy.transform.position;
            enemy.selfBody.velocity = dir.normalized * swimSpeed;

            if (timer > attackLength)
            {
                timer = 0.0f;
                phase = attackPhase.Recovering;
            }
        }
        if (phase == attackPhase.Recovering)
        {
            if (timer > recoveryLength)
            {
                timer = 0.0f;
                phase = attackPhase.Charging;
            }
        }
    }

    override public void Exit()
    {
    }
}
