using UnityEngine;
using System.Collections;

public class UrchinFrozenState : State<Urchin>
{
    private Urchin enemy;

    public UrchinFrozenState(Urchin enemyInstance, StateMachine<Urchin> fsm)
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
