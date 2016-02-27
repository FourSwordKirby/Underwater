using UnityEngine;
using System.Collections;

public class GroundedChecker : MonoBehaviour {
    public Player owner;

    public LayerMask floorMask;

    public float checkDistance;

    void Update()
    {
        //Note, might just make it raycast when the player isn't grounded
        Debug.DrawRay(this.transform.position, checkDistance * Vector3.down, Color.red);

        if (Physics2D.Raycast(this.transform.position, Vector3.down, checkDistance, floorMask))
        {
            owner.grounded = true;
        }
        else
        {
            owner.grounded = false;
        }
    }
}
