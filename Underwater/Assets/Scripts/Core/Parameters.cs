using UnityEngine;
using System.Collections;

public class Parameters : MonoBehaviour {

    public enum InputDirection
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
        Stop
    };

    public enum Direction
    {
        Left,
        Right
    }

    public enum Aim
    {
        Up,
        TiltUp,
        Neutral,
        TiltDown,
        Down
    }

    //Do we need this?
    public enum PlayerStatus
    {
        Default, //Normal everyday state
        Immovable, //Not affected by forces
        Invulnerable, //Doesn't take damage, can be moved around (reduced knockback?)
        Invincible, //No damageno knockback
        Counter //Can initiate a counter attack
    };

    public enum HurtboxStatus
    {
        Superarmor,
        invuln
    }

    public enum DamageEffect
    {
        None,
        Freeze
    }

    public static Direction VectorToDir(Vector2 inputVector)
    {
        if (inputVector.x > 0)
            return Direction.Right;
        else
            return Direction.Left;
    }

    public static Aim VectorToAim(Vector2 inputVector)
    {
        float xMag = Mathf.Abs(inputVector.x);
        float yMag = Mathf.Abs(inputVector.y);
        if (inputVector.y > 0 && yMag > 1.5f * xMag)
            return Aim.Up;
        else if (inputVector.y > 0 && yMag < 1.5f * xMag) 
                return Aim.TiltUp;
        if (inputVector.y == 0.0)
            return Aim.Neutral;
        else if (inputVector.y < 0 && yMag < 1.5f * xMag)
            return Aim.TiltDown;
        else if (inputVector.y < 0 && yMag > 1.5f * xMag) 
            return Aim.Down;
        return Aim.Neutral;
    }

    public static InputDirection PlayerOrientationToDir(Direction playerDir, Aim playerAim)
    {
        return InputDirection.East;
    }

    public static Vector2 InputDirToVector(InputDirection dir)
    {
        switch (dir)
        {
            case Parameters.InputDirection.North:
                return new Vector2(0, 1);
            case Parameters.InputDirection.NorthEast:
                return new Vector2(Mathf.Sin(Mathf.PI / 2), Mathf.Sin(Mathf.PI / 2));
            case Parameters.InputDirection.East:
                return new Vector2(1, 0);
            case Parameters.InputDirection.SouthEast:
                return new Vector2(Mathf.Sin(Mathf.PI / 2), Mathf.Sin(3 * Mathf.PI / 2));
            case Parameters.InputDirection.South:
                return new Vector2(0, -1);
            case Parameters.InputDirection.SouthWest:
                return new Vector2(Mathf.Sin(3 * Mathf.PI / 2), Mathf.Sin(3 * Mathf.PI / 2));
            case Parameters.InputDirection.West:
                return new Vector2(-1, 0);
            case Parameters.InputDirection.NorthWest:
                return new Vector2(Mathf.Sin(3 * Mathf.PI / 2), Mathf.Sin(Mathf.PI / 2));
        }
        return Vector2.zero;
    }


    public static int GetDirAnimation(Direction playerDir)
    {
        switch (playerDir)
        {
            case Parameters.Direction.Left:
                return -1;
            case Parameters.Direction.Right:
                return 1;
        }
        return 0;
    }

    public static int GetAimAnimation(Aim playerAim)
    {
        switch (playerAim)
        {
            case Parameters.Aim.Down:
                return -2;
            case Parameters.Aim.TiltDown:
                return -1;
            case Parameters.Aim.Neutral:
                return 0;
            case Parameters.Aim.TiltUp:
                return 1;
            case Parameters.Aim.Up:
                return 2;
        }
        return 0;
    }



    public static bool isOppositeDirection(InputDirection dir_1, InputDirection dir_2)
    {
        switch (dir_1)
        {
            case InputDirection.North: 
                return (dir_2 == InputDirection.SouthEast || dir_2 == InputDirection.South || dir_2 == InputDirection.SouthWest);
            case InputDirection.NorthEast: 
                return (dir_2 == InputDirection.West || dir_2 == InputDirection.South || dir_2 == InputDirection.SouthWest);
            case InputDirection.East: 
                return (dir_2 == InputDirection.NorthWest || dir_2 == InputDirection.West || dir_2 == InputDirection.SouthWest);
            case InputDirection.SouthEast: 
                return (dir_2 == InputDirection.NorthWest || dir_2 == InputDirection.West || dir_2 == InputDirection.North);
            case InputDirection.South: 
                return (dir_2 == InputDirection.NorthEast || dir_2 == InputDirection.North || dir_2 == InputDirection.NorthWest);
            case InputDirection.SouthWest: 
                return (dir_2 == InputDirection.East || dir_2 == InputDirection.North || dir_2 == InputDirection.NorthEast);
            case InputDirection.West: 
                return (dir_2 == InputDirection.SouthEast || dir_2 == InputDirection.East || dir_2 == InputDirection.NorthEast);
            case InputDirection.NorthWest: 
                return (dir_2 == InputDirection.SouthEast || dir_2 == InputDirection.South || dir_2 == InputDirection.East);
        }
        return false;
    }

    public static InputDirection getOppositeDirection(InputDirection dir)
    {
        switch (dir)
        {
            case InputDirection.North:
                return InputDirection.South;
            case InputDirection.NorthEast:
                return InputDirection.SouthWest;
            case InputDirection.East:
                return InputDirection.West;
            case InputDirection.SouthEast:
                return InputDirection.NorthWest;
            case InputDirection.South:
                return InputDirection.North;
            case InputDirection.SouthWest:
                return InputDirection.NorthEast;
            case InputDirection.West:
                return InputDirection.East;
            case InputDirection.NorthWest:
                return InputDirection.SouthEast;
        }
        return InputDirection.Stop;
    }

    

    public static InputDirection getTargetDirection(Mobile player, Mobile target)
    {
        Vector2 playerPos = player.transform.position; 
        Vector2 targetPos = target.transform.position;

        Vector2 dir = targetPos - playerPos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle >= -22.5 && angle < 22.5)
        {
            return Parameters.InputDirection.East;
        }
        else if (angle >= 22.5 && angle < 67.5)
        {
            return Parameters.InputDirection.NorthEast;
        }
        else if (angle >= 67.5 && angle < 112.5)
        {
            return Parameters.InputDirection.North;
        }
        else if (angle >= 112.5 && angle < 157.5)
        {
            return Parameters.InputDirection.NorthWest;
        }
        else if (angle >= 157.5 || angle < -157.5)
        {
            return Parameters.InputDirection.West;
        }
        else if (angle >= -157.5 && angle < -112.5)
        {
            return Parameters.InputDirection.SouthWest;
        }
        else if (angle >= -112.5 && angle < -67.5)
        {
            return Parameters.InputDirection.South;
        }
        else if (angle >= -67.5 && angle < -22.5)
        {
            return Parameters.InputDirection.SouthEast;
        }


        return Parameters.InputDirection.Stop;
    }
}
