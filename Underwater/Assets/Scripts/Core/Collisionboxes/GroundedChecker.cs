using UnityEngine;
using System.Collections;

public class GroundedChecker : MonoBehaviour {
    public Player owner;

    public LayerMask floorMask;

    public GameObject leftCheck;
    public GameObject rightCheck;

    public float checkDistance;

    void Update()
    {
        //Note, might just make it raycast when the player isn't grounded
        Debug.DrawRay(leftCheck.transform.position, checkDistance * Vector3.down, Color.red);

        if (Physics2D.Raycast(leftCheck.transform.position, Vector3.down, checkDistance, floorMask) ||
            Physics2D.Raycast(rightCheck.transform.position, Vector3.down, checkDistance, floorMask))
        {
            owner.grounded = true;
        }
        else
        {
            owner.grounded = false;
        }
    }
}
