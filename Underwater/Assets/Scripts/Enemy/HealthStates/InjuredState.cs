using UnityEngine;
using System.Collections;

public class InjuredState : State<Enemy> {

    private Enemy enemy;
    /*Amount of time in seconds before the enemy starts trying to regen health again*/
    private float coolDownPeriod;
    private float counter;

    public InjuredState(Enemy enemyInstance, StateMachine<Enemy> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
        coolDownPeriod = 0.5f;
    }

    override public void Enter()
    {
        return;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        counter += Time.deltaTime;
        if(counter > coolDownPeriod);
            enemy.StatusFsm.ChangeState(new DefaultState(enemy, enemy.StatusFsm));
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
