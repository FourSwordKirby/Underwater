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

        if (player.grounded)
        {
            player.selfBody.velocity = new Vector2(player.selfBody.velocity.x, player.jumpHeight);
            player.grounded = false;
        }

        player.anim.SetBool("Airborne", true);

        return;
    }


    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        movementInputVector = Controls.getDirection(player);

        //Check if the player is grounded. If they are transition back into the grounded state
        if (player.grounded && leewayFrames <= 0)
        {
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
            return;
        }

        //Doing air boosts
        if (Controls.JumpInputDown() && player.isUnderWater && player.jetpackFuel > 0)
        {
            player.ActionFsm.ChangeState(new BoostState(player, player.ActionFsm));
            return;
        }

        leewayFrames--;
    }

    override public void FixedExecute()
    {
        float xVelocity = Mathf.Clamp(player.selfBody.velocity.x + movementInputVector.x * player.airDrift,
                                        -player.airMovementSpeed,
                                        player.airMovementSpeed);
        float yVelocity = player.selfBody.velocity.y;

        //Used for variable jump height
        if (!Controls.JumpInputHeld() && yVelocity > 0)
        {
            yVelocity = yVelocity * 0.9f;
        }

        player.selfBody.velocity = new Vector2(xVelocity, yVelocity);
    }

    override public void Exit()
    {
    }
}
