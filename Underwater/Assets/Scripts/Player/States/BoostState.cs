using UnityEngine;
using System.Collections;

public class BoostState : State<Player>
{
    private Player player;

    Vector2 movementInputVector;
    Vector2 jetPackDirection;

    public BoostState(Player playerInstance, StateMachine<Player> fsm)
        : base(playerInstance, fsm)
    {
        player = playerInstance;
    }

    override public void Enter()
    {
        player.anim.SetBool("Airborne", true);
    }

    /*error with collision boxes puts player in air state when he actually isn't*/
    override public void Execute()
    {
        movementInputVector = Controls.getDirection();

        if (movementInputVector != Vector2.zero)
        {
            if (Mathf.Abs(movementInputVector.x) > Mathf.Abs(movementInputVector.y))
            {
                if (movementInputVector.x < 0)
                    jetPackDirection = Vector2.left;
                else
                    jetPackDirection = Vector2.right;
            }
            else
            {
                if (movementInputVector.y < 0)
                    jetPackDirection = Vector2.down;
                else
                    jetPackDirection = Vector2.up;
            }
        }
        else
        {
            jetPackDirection = Vector2.up;
        }

        //Check if the player is grounded.
        if (player.grounded)
        {
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
            return;
        }

        if (!Controls.JumpInputHeld() || player.jetpackFuel < 0)
        {
            player.ActionFsm.ChangeState(new AirState(player, player.ActionFsm));
            return;
        }

        //Actually use the fuel
        player.UseFuel(Time.deltaTime);

        //Gotta animate the jetpacktrail
    }

    override public void FixedExecute()
    {
        player.selfBody.velocity = jetPackDirection.normalized * player.jetpackSpeed;
    }

    override public void Exit()
    {
    }
}
