using UnityEngine;
using System.Collections;

public class BossIdleState : State<Boss>
{
    private Boss boss;

    private float phaseLength = 2.0f;
    private float timer;
    private Boss.BossStates nextState;

    public BossIdleState(Boss enemyInstance, StateMachine<Boss> fsm, Boss.BossStates nextState)
        : base(enemyInstance, fsm)
    {
        boss = enemyInstance;
        this.nextState = nextState;
    }

    override public void Enter()
    {
        Debug.Log(nextState);
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        timer += Time.deltaTime;

        if (timer > phaseLength)
        {
            if(nextState == Boss.BossStates.Swipe)
                boss.ActionFsm.ChangeState(new BossSwipeState(boss, boss.ActionFsm));
            else if(nextState == Boss.BossStates.Suction)
                boss.ActionFsm.ChangeState(new BossSuctionState(boss, boss.ActionFsm));
            else if (nextState == Boss.BossStates.Summon)
                boss.ActionFsm.ChangeState(new BossSummonState(boss, boss.ActionFsm, 2));
        }

        Debug.Log("Idling");
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}