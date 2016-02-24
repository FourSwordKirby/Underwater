using UnityEngine;
using System.Collections;

public class Controls {

    /*These constants refer to specific thresholds for reading in inputs
     * For example, the constant FALL_THROUGH_THRESHOLD denotes the threshold 
     * between crouching on a platform and falling through the platform
     */
    public const float FALL_THROUGH_THRESHOLD = 0.5f;

    public static Vector2 getDirection(Player player)
    {
        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("P1 Horizontal")) > Mathf.Abs(Input.GetAxis("P1 Keyboard Horizontal")))
            xAxis = Input.GetAxis("P1 Horizontal");
        else
            xAxis = Input.GetAxis("P1 Keyboard Horizontal");
        if (Mathf.Abs(Input.GetAxis("P1 Vertical")) > Mathf.Abs(Input.GetAxis("P1 Keyboard Vertical")))
            yAxis = Input.GetAxis("P1 Vertical");
        else
            yAxis = Input.GetAxis("P1 Keyboard Vertical");

        return new Vector2(xAxis, yAxis);
    }

    public static Parameters.InputDirection getInputDirection(Player player)
    {
        return Parameters.vectorToDirection(getDirection(player));
    }

    public static bool jumpInputDown(Player player)
    {
        return Input.GetButtonDown("P1 Jump");
    }

    public static bool attackInputDown(Player player)
    {
        return Input.GetButtonDown("P1 Attack");
    }

    public static bool specialInputDown(Player player)
    {
        return Input.GetButtonDown("P1 Special");
    }

    public static bool shieldInputDown(Player player)
    {
        return Input.GetButtonDown("P1 Shield");
    }

    public static bool enhanceInputDown(Player player)
    {
        return Input.GetButtonDown("P1 Enhance");
    }

    public static bool superInputDown(Player player)
    {
        return Input.GetButtonDown("P1 Super");
    }

    public static bool pauseInputDown(Player player)
    {
        return Input.GetButtonDown("Pause");
    }


    public static bool jumpInputHeld(Player player)
    {
        return Input.GetButton("P1 Jump");
    }

    public static bool attackInputHeld(Player player)
    {
        return Input.GetButton("P1 Attack");
    }

    public static bool specialInputHeld(Player player)
    {
        return Input.GetButton("P1 Special");
    }

    public static bool shieldInputHeld(Player player)
    {
        return Input.GetButton("P1 Shield");
    }

    public static bool enhanceInputHeld(Player player)
    {
        return Input.GetButton("P1 Enhance");
    }

    public static bool superInputHeld(Player player)
    {
        return Input.GetButton("P1 Super");
    }

    public static bool pauseInputHeld(Player player)
    {
        return Input.GetButton("Pause");
    }
}
