using UnityEngine;
using System.Collections;

public class BossSummonState : State<Boss>
{
    private Boss boss;
    private int enemyCount;

    private int currentEnemyCount = 1;

    private float phaseLength = 2.0f;
    private float timer;


    public BossSummonState(Boss enemyInstance, StateMachine<Boss> fsm, int enemyCount)
        : base(enemyInstance, fsm)
    {
        boss = enemyInstance;
        this.enemyCount = enemyCount;
    }

    override public void Enter()
    {
        boss.anim.SetTrigger("summon");
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        timer += Time.deltaTime;

        if (timer > currentEnemyCount * phaseLength)
        {
            GameObject spawnedEnemy = GameObject.Instantiate(boss.spawnableEnemies[0]);
            spawnedEnemy.transform.position = boss.mouthPosition.transform.position;
            if (spawnedEnemy.GetComponent<Squid>() != null)
            {
                Squid squid = spawnedEnemy.GetComponent<Squid>();
                squid.selfBody.velocity = (Vector2.left+ new Vector2(0, Random.Range(-0.5f, 0.5f))) * 4.0f;
            }
            currentEnemyCount++;
            if(currentEnemyCount < enemyCount)
                boss.anim.SetTrigger("summon");
        }

        if (timer > enemyCount * phaseLength)
        {
            boss.ActionFsm.ChangeState(new BossIdleState(boss, boss.ActionFsm, Boss.BossStates.Swipe));
        }
        Debug.Log("Summoning");
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}