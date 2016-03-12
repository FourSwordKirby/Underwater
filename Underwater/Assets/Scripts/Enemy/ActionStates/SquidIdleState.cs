using UnityEngine;
using System.Collections;

public class SquidIdleState : State<SquidEnemy>
{
    private SquidEnemy enemy;
    private float startingHeight;
    private float jumpHeight = 6.0f;

    public SquidIdleState(SquidEnemy enemyInstance, StateMachine<SquidEnemy> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
        enemy.currentTarget = null;
        this.startingHeight = enemy.transform.position.y;
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
        if (enemy.transform.position.y <= startingHeight)
            enemy.selfBody.velocity = new Vector2(0, jumpHeight);
        Debug.Log("Idling");
    }

    override public void Exit()
    {
    }
}
