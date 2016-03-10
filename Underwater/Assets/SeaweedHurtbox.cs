using UnityEngine;
using System.Collections;

public class SeaweedHurtbox : Hurtbox {

    public Seaweed owner;

    override public void TakeDamage(float damage)
    {
    }

    override public void TakeHit(float hitlag, float hitstun, Vector2 knockback)
    {
    }

    override public void ApplyEffect(Parameters.DamageEffect effect)
    {
        if (effect == Parameters.DamageEffect.Freeze)
        {
            owner.Freeze();
        }
        else
        {
            owner.Retract();
        }
    }
}
