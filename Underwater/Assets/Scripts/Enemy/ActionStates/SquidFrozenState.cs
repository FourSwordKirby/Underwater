using System;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Squids should float. When the player enter's their range, they should charge up and then spin towards the player
/// </summary>
public class SquidFrozenState : State<SquidEnemy>
{
    private SquidEnemy enemy;

    public SquidFrozenState(SquidEnemy enemyInstance, StateMachine<SquidEnemy> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
    }

    override public void Enter()
    {
        enemy.selfBody.velocity = Vector2.zero;
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
