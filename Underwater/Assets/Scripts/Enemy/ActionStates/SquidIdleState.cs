using UnityEngine;
using System.Collections;

/// <summary>
/// Squids should float. When the player enter's their range, they should charge up and then spin towards the player
/// </summary>
public class SquidIdleState : State<SquidEnemy>
{
    private Enemy enemy;

    public SquidIdleState(SquidEnemy enemyInstance, StateMachine<SquidEnemy> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
    }

    override public void Enter()
    {
        return;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {

    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
