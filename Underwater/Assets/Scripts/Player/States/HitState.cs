using UnityEngine;
using System.Collections;

public class HitState : State<Player>
{

    private Player player;
    private float hitlag;
    private float hitstun;
    private Vector2 knockback;

    public HitState(Player playerInstance, float hitlag, float hitstun, Vector2 knockback, StateMachine<Player> fsm)
        : base(playerInstance, fsm)
    {
        player = playerInstance;

        //probably need some means for denoting tumble etc.

        this.hitlag = hitlag;
        this.hitstun = hitstun;
        this.knockback = knockback;
    }

    override public void Enter()
    {
        //Go into a hit animation
        Debug.Log("I've been hit");

        player.invulnTime = 1.0f;

        player.environmentCollisionBox.gameObject.layer = LayerMask.NameToLayer("Invuln");
        player.hitboxManager.deactivateAllHitboxes();


        player.selfBody.velocity += knockback;
    }

    override public void Execute()
    {
        if (hitstun > 0)
        {
            hitstun -= Time.deltaTime;

            if (hitstun <= 0)
            {
                player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
            }
            return;
        }
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}

