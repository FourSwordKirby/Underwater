﻿using UnityEngine;
using System.Collections;

public class DestroyableWall : Hurtbox {
    override public void TakeDamage(float damage)
    {
    }

    override public void TakeHit(float hitlag, float hitstun, Vector2 knockback)
    {
    }

    override public void ApplyEffect(Parameters.DamageEffect effect)
    {
        if (effect == Parameters.DamageEffect.Blast)
        {
            Destroy(this.gameObject);
        }
    }
}
