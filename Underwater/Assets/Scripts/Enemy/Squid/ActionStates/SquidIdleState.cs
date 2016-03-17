using UnityEngine;
using System.Collections;

public class SquidIdleState : State<Squid>
{
    private Squid enemy;
    private float jumpHeight = 6.0f;

    public SquidIdleState(Squid enemyInstance, StateMachine<Squid> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
        enemy.currentTarget = null;
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
        Quaternion targetRotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
        enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, targetRotation, 60 * Time.deltaTime);

        if (Mathf.Abs(enemy.transform.rotation.eulerAngles.z) < 1.0f)
            enemy.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);

        if (enemy.transform.position.y <= enemy.startingHeight)
        {
            enemy.selfBody.velocity += new Vector2(0, jumpHeight) - new Vector2(0, enemy.selfBody.velocity.y);
        }
    }

    override public void Exit()
    {
    }
}
