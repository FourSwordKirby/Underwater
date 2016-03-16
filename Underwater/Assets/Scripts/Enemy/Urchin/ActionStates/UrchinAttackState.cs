using UnityEngine;
using System.Collections;

public class UrchinAttackState : State<Urchin>
{
    private Urchin enemy;

    private float chargeLength = 3.5f;
    private float spikeSpeed = 4.0f;
    private float timer;

    public UrchinAttackState(Urchin enemyInstance, StateMachine<Urchin> fsm)
        : base(enemyInstance, fsm)
    {
        enemy = enemyInstance;
    }

    override public void Enter()
    {
        timer = 2.5f;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
    }

    override public void FixedExecute()
    {
        timer += Time.deltaTime;
        //prepping to attack player
        if (timer > chargeLength)
        {
            timer = 0.0f;
            UrchinSpikeHitbox spikeLeft = GameObject.Instantiate(enemy.ammo);
            spikeLeft.transform.position = enemy.gameObject.transform.position;
            spikeLeft.transform.Rotate(0, 0, 90.0f);
            spikeLeft.selfBody.velocity = Vector2.left * spikeSpeed;

            UrchinSpikeHitbox spikeUp = GameObject.Instantiate(enemy.ammo);
            spikeUp.transform.position = enemy.gameObject.transform.position;
            spikeUp.transform.Rotate(0, 0, 0);
            spikeUp.selfBody.velocity = Vector2.up * spikeSpeed;

            UrchinSpikeHitbox spikeRight = GameObject.Instantiate(enemy.ammo);
            spikeRight.transform.position = enemy.gameObject.transform.position;
            spikeRight.transform.Rotate(0, 0, -90.0f);
            spikeRight.selfBody.velocity = Vector2.right * spikeSpeed;
    
            AudioSource.PlayClipAtPoint(enemy.audio[0], enemy.transform.position);
        }
    }

    override public void Exit()
    {
    }
}
