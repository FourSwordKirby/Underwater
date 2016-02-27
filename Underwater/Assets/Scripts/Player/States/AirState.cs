using UnityEngine;
using System.Collections;

public class AirState : State<Player>
{
    private Player player;
    private Vector2 movementInputVector;

    //This is used to prevent the game from declaring the player as grounded too soon.
    private int leewayFrames = 3;

    public AirState(Player playerInstance, StateMachine<Player> fsm)
        : base(playerInstance, fsm)
    {
        player = playerInstance;
    }

    override public void Enter()
    {
        player.LockDirection();

        if(player.grounded)
            player.selfBody.velocity = new Vector2(player.selfBody.velocity.x, player.jumpHeight);

        player.anim.SetBool("Airborne", true);

        return;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        movementInputVector = Controls.getDirection(player);

        //Might want to change this stuff later to include transition states
        //Check if the player is grounded.
        if (player.grounded && leewayFrames <= 0)
        {
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
            return;
        }

        //Doing air boosts
        if (Controls.jumpInputDown() && player.isUnderWater && player.jetpackFuel > 0)
        {
            player.ActionFsm.ChangeState(new BoostState(player, player.ActionFsm));
            return;
        }

        leewayFrames--;
    }

    override public void FixedExecute()
    {
        player.selfBody.velocity = new Vector2(movementInputVector.x * player.airMovementSpeed, player.selfBody.velocity.y);
    }

    override public void Exit()
    {
    }
}
