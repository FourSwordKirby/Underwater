using UnityEngine;
using System.Collections;

public class IdleState : State<Player> {

    private Player player;

    public IdleState(Player playerInstance, StateMachine<Player> fsm)
        : base(playerInstance, fsm)
    {
        player = playerInstance;
    }

    override public void Enter(){
        player.UnlockDirection();

        player.RefillFuel();

        player.anim.SetFloat("MoveSpeed", 0.0f);
        player.anim.SetBool("Airborne", false);
    }

    override public void Execute()
    {
        Vector2 movementInputVector = Controls.getDirection(player);

        //Moving
        if (movementInputVector.x != 0)
        {
            player.ActionFsm.ChangeState(new MovementState(player, player.ActionFsm));
            return;
        }

        //Jumping
        if (Controls.JumpInputDown())
        {
            player.ActionFsm.ChangeState(new AirState(player, player.ActionFsm));
            return;
        }

        //falling through platforms
        //Fix this later jesus christ
        /*
        if (Controls.getDirection(player).y < -Controls.FALL_THROUGH_THRESHOLD)
        {
            player.grounded = false;
            player.environmentCollisionBox.fallThrough();
            player.ActionFsm.ChangeState(new AirState(player, player.ActionFsm));
            return;
        }
         */
    }

    override public void FixedExecute(){    }

    override public void Exit(){    }
}
