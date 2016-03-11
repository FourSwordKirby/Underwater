using UnityEngine;
using System.Collections;

public class SquidHitbox : Hitbox {

    void OnTriggerEnter2D(Collider2D col)
    {
        //Basic normal bubble stuff
        Hurtbox hurtbox = col.gameObject.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            hurtbox.TakeDamage(damage);
            hurtbox.TakeHit(0, hitstun, knockbackVector);
            hurtbox.ApplyEffect(this.effect);
        }
    }
}
