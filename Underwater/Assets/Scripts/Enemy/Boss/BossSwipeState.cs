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
                if (nextState < 0.4f)
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Swipe));
                else
                {
                    Debug.Log("here");
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Suction));
                }
            }
            else
            {
                if (nextState < 0.3f)
                    boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Swipe));
                else if (0.3f < nextState && nextState < 0.5f)
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