using UnityEngine;
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
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        UrchinSpikeHitbox spike = GameObject.Instantiate(enemy.ammo);
        spike.transform.position = enemy.gameObject.transform.position;
        spike.selfBody.velocity = Vector2.left;
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
