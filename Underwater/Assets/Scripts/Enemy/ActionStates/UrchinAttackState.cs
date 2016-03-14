﻿using UnityEngine;
using System.Collections;

public class UrchinAttackState : State<Urchin>
{
    private Urchin enemy;

    public UrchinAttackState(Urchin enemyInstance, StateMachine<Urchin> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
    }

    override public void Enter()
    {
        enemy.selfBody.gravityScale = 0.4f;
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
