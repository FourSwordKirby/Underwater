using UnityEngine;
using System.Collections;

public class SquidAttackRange : MonoBehaviour {

    private SquidEnemy owner;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            //Change to an attacking state
            owner.currentTarget = hurtbox.owner.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null && hurtbox.owner.gameObject == owner.currentTarget.gameObject)
        {
            //Change to an idle state
            owner.currentTarget = null;
        }
    }
}
