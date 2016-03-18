using UnityEngine;
using System.Collections;

public class BossSwipeState : State<Boss>
{
    private Boss boss;

    private float phaseLength = 2.0f;
    private float timer;

    public BossSwipeState(Boss enemyInstance, StateMachine<Boss> fsm)
        : base(enemyInstance, fsm)
    {
        boss = enemyInstance;
    }

    override public void Enter()
    {
        boss.anim.SetTrigger("swipe");
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        timer += Time.deltaTime;

        if (timer > phaseLength)
        {
            float nextState = Random.Range(0.0f, 1.0f);
            if (!boss.injured)
            {
                if (nextState < 1.0f / 2.0f)
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Swipe));
                else
                {
                    Debug.Log("here");
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Suction));
                }
            }
            else
            {
                if (nextState < 1.0f / 2.0f)
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Swipe));
                else if (1.0f / 2.0f < nextState && nextState < 3.0f / 4.0f)
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Summon));
                else
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Suction));
            }
        }

        Debug.Log("Swipping");
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}