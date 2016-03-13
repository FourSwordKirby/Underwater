﻿using UnityEngine;
using System.Collections;

public class EnemyHurtbox : Hurtbox {
    public Enemy owner;


    override public void TakeDamage(float damage)
    {
        owner.health -= damage;
    }

    override public void TakeHit(float hitlag, float hitstun, Vector2 knockback)
    {
        owner.selfBody.velocity = knockback;
    }

    override public void ApplyEffect(Parameters.DamageEffect effect)
    {
        if (effect == Parameters.DamageEffect.Freeze)
        {
            if (owner.health < owner.maxHealth * 0.5f)
            {
                owner.Freeze();
            }
        }
    }
}
