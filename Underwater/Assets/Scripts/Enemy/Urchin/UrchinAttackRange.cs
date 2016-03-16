using UnityEngine;
using System.Collections;

public class UrchinAttackRange : MonoBehaviour {

    public Urchin owner;

    public float deaggroLength;
    public float deaggroTimer;

    void Update()
    {
        if (deaggroTimer > 0)
        {
            deaggroTimer -= Time.deltaTime;
            if (deaggroTimer <= 0)
                owner.ActionFsm.ChangeState(new UrchinIdleState(owner, owner.ActionFsm));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            //Change to an attacking state
            owner.ActionFsm.ChangeState(new UrchinAttackState(owner, owner.ActionFsm));
            deaggroTimer = 0;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            //start the deaggro timer
            deaggroTimer = deaggroLength;
        }
    }
}
