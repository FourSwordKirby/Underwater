using UnityEngine;
using System.Collections;

public class PlayerHurtbox : Hurtbox {
    public Player owner;

    override public void TakeDamage(float damage)
    {
        owner.LoseHealth(damage);
        AudioSource.PlayClipAtPoint(owner.audio[5], owner.transform.position);
    }

    override public void TakeHit(float hitlag, float hitstun, Vector2 knockback)
    {
        if(hitstun > 0)
            owner.ActionFsm.ChangeState(new HitState(owner, hitlag, hitstun, knockback, owner.ActionFsm));
        else
            owner.selfBody.velocity = knockback;

        Camera.main.GetComponent<CameraControls>().Shake();
    }

    override public void ApplyEffect(Parameters.DamageEffect effect)
    {
    }
}
