using UnityEngine;
using System.Collections;

public class BossSuctionState : State<Boss>
{
    private Boss boss;

    private float phaseLength=5.0f;
    private float timer;

    public BossSuctionState(Boss enemyInstance, StateMachine<Boss> fsm)
        : base(enemyInstance, fsm)
    {
        boss = enemyInstance;
    }

    override public void Enter()
    {
        boss.anim.SetTrigger("suction");
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        timer += Time.deltaTime;

        if (timer > phaseLength / 5)
        {
            boss.inhaleHitbox.gameObject.SetActive(true);
        }

        if (timer > 4 * phaseLength / 5)
        {
            boss.inhaleHitbox.gameObject.SetActive(false);
        }

        if (timer > phaseLength)
        {
            boss.ActionFsm.ChangeState(new BossSummonState(boss, boss.ActionFsm, 1));
        }
        Debug.Log("Suctioning");
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
