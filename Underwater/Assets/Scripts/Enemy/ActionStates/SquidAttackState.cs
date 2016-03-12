using UnityEngine;
using System.Collections;

/// <summary>
/// Squids should float. When the player enter's their range, they should charge up and then spin towards the player
/// </summary>
public class SquidAttackState : State<SquidEnemy>
{
    private SquidEnemy enemy;

    public SquidAttackState(SquidEnemy enemyInstance, StateMachine<SquidEnemy> fsm, GameObject currentTarget)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
        enemy.currentTarget = currentTarget;
    }

    override public void Enter()
    {
        return;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        Vector3 dir = enemy.transform.position-enemy.currentTarget.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, targetRotation, 60 * Time.deltaTime);
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
