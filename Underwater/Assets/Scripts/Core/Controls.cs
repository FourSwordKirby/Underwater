using UnityEngine;
using System.Collections;

public class Controls {

    /*These constants refer to specific thresholds for reading in inputs
     * For example, the constant FALL_THROUGH_THRESHOLD denotes the threshold 
     * between crouching on a platform and falling through the platform
     */
    public const float FALL_THROUGH_THRESHOLD = 0.5f;

    public static Vector2 getDirection()
    {
        return getDirection(null);
    }

    public static Vector2 getDirection(Player player)
    {
        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Keyboard Horizontal")))
            xAxis = Input.GetAxis("Horizontal");
        else
            xAxis = Input.GetAxis("Keyboard Horizontal");
        if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Keyboard Vertical")))
            yAxis = Input.GetAxis("Vertical");
        else
            yAxis = Input.GetAxis("Keyboard Vertical");
        return new Vector2(xAxis, yAxis);
    }

    public static bool JumpInputDown()
    {
        return Input.GetButtonDown("Jump");
    }

    public static bool ShootInputDown(Player player)
    {
        return Input.GetButtonDown("Shoot");
    }

    public static bool NextWeaponInputDown()
    {
        return Input.GetButtonDown("Next Weapon");
    }

    public static bool PrevWeaponInputDown()
    {
        return Input.GetButtonDown("Prev Weapon");
    }

    public static bool Toggle1Down()
    {
        return Input.GetButtonDown("Toggle 1");
    }

    public static bool AimUpInputDown(Player player)
    {
        return Input.GetButtonDown("Aim Up");
    }

    public static bool AimDownInputDown(Player player)
    {
        return Input.GetButtonDown("AimDown");
    }

    public static bool InteractInputDown()
    {
        return Input.GetButtonDown("Interact");
    }

    public static bool pauseInputDown(Player player)
    {
        return Input.GetButtonDown("Pause");
    }


    public static bool JumpInputHeld()
    {
        return Input.GetButton("Jump");
    }

    public static bool ShootInputHeld()
    {
        return Input.GetButton("Shoot");
    }

    public static bool NextWeaponInputHeld()
    {
        return Input.GetButton("Next Weapon");
    }

    public static bool PrevWeaponInputHeld()
    {
        return Input.GetButton("Prev Weapon");
    }

    public static bool AimUpInputHeld()
    {
        return Input.GetButton("Aim Up");
    }

    public static bool AimDownInputHeld()
    {
        return Input.GetButton("Aim Down");
    }

    public static bool InteractInputHeld()
    {
        return Input.GetButton("Interact");
    }

    public static bool pauseInputHeld()
    {
        return Input.GetButton("Pause");
    }
}
