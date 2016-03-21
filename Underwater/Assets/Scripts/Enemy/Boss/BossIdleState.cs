using UnityEngine;
using System.Collections;

public class BossIdleState : State<Boss>
{
    private Boss boss;

    private float phaseLength = 2.0f;
    private float timer;
    private Boss.BossStates nextState;

    private int direction;

    public BossIdleState(Boss enemyInstance, StateMachine<Boss> fsm, Boss.BossStates nextState)
        : base(enemyInstance, fsm)
    {
        boss = enemyInstance;
        this.nextState = nextState;
    }

    override public void Enter()
    {
        if (Random.RandomRange(0.0f, 1) > 0.5f)
            direction = 1;
        else
            direction = -1;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        timer += Time.deltaTime;

        if (timer > phaseLength / 2)
        {
            if (!(boss.startingX - boss.moveRange < boss.transform.position.x && boss.transform.position.x < boss.startingX + boss.moveRange))
                direction *= -1;
            boss.selfBody.velocity = Vector2.right * direction;
        }

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
        boss.selfBody.velocity = Vector2.zero; ;
    }
}