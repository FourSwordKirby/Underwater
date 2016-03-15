using UnityEngine;
using System.Collections;

public class BoostState : State<Player>
{
    private Player player;
    private float trailDisplacement = 0.5f;


    Vector2 movementInputVector;
    Vector2 jetPackDirection;

    private float jetpackTrailCooldownTime = 0.05f;
    private float timer;

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

        if (!player.isUnderWater || !Controls.JumpInputHeld() || player.jetpackFuel < 0)
        {
            player.ActionFsm.ChangeState(new AirState(player, player.ActionFsm));
            return;
        }

        //Actually use the fuel
        player.UseFuel(Time.deltaTime);

        //Gotta animate the jetpacktrail
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GameObject boostTrail = GameObject.Instantiate(player.prefabs[0]);
            Vector3 offset = Vector3.zero;
            if (jetPackDirection.x == 0)
                offset = new Vector3(0, -Mathf.Sign(jetPackDirection.y), 0);
            if (jetPackDirection.y == 0)
                offset = new Vector3(-Mathf.Sign(jetPackDirection.x), 0, 0);

            boostTrail.transform.position = player.transform.position + offset * trailDisplacement;

            //making sfx
            AudioSource.PlayClipAtPoint(player.audio[4], player.transform.position);

            timer = jetpackTrailCooldownTime;
        }
    }

    override public void FixedExecute()
    {
        player.selfBody.velocity = jetPackDirection.normalized * player.jetpackSpeed;
    }

    override public void Exit()
    {
    }
}
