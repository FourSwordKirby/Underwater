using UnityEngine;
using System.Collections;

public class SquidAttackRange : MonoBehaviour {

    public Squid owner;

    public float deaggroLength;
    public float deaggroTimer;

    void Update()
    {
        if (deaggroTimer > 0)
        {
            deaggroTimer -= Time.deltaTime;
            if(deaggroTimer <= 0)
                owner.ActionFsm.ChangeState(new SquidIdleState(owner, owner.ActionFsm));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            //Change to an attacking state
            owner.ActionFsm.ChangeState(new SquidAttackState(owner, owner.ActionFsm, hurtbox.owner.gameObject));
            deaggroTimer = 0;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null && hurtbox.owner.gameObject == owner.currentTarget.gameObject)
        {
            //start the deaggro timer
            deaggroTimer = deaggroLength;
        }
    }
}
