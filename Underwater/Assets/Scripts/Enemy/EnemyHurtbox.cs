using UnityEngine;
using System.Collections;

public class EnemyHurtbox : Hurtbox {
    public Enemy owner;

    private Color previousColor;
    private Color hitColor = Color.red;
    private float hitFlashLength = 0.2f;
    private float timer;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            owner.spriteRenderer.color = hitColor;
            if (timer <= 0)
                owner.spriteRenderer.color = previousColor;
        }
    }

    override public void TakeDamage(float damage)
    {
        owner.TakeDamage(damage);
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

        if (owner.spriteRenderer.color != hitColor)
            previousColor = owner.spriteRenderer.color;
        timer = hitFlashLength;
    }
}
