using UnityEngine;
using System.Collections;

public class DefaultState : State<Enemy>
{
    private Enemy enemy;

    public DefaultState(Enemy enemyInstance, StateMachine<Enemy> fsm)
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
        if (enemy.health < enemy.maxHealth && enemy.health > 0)
        {
            enemy.health = Mathf.Clamp(enemy.health + enemy.healthRegenRate * Time.deltaTime, 0, enemy.maxHealth);
            if (enemy.health == enemy.maxHealth)
                enemy.Unfreeze();
        }
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
