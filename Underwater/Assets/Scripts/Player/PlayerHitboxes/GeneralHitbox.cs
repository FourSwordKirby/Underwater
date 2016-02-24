using UnityEngine;
using System.Collections;

public class GeneralHitbox : Hitbox {

    void OnTriggerEnter2D(Collider2D col)
    {
        Hurtbox hurtbox = col.gameObject.GetComponent<Hurtbox>();
        if (hurtbox != null && hurtbox.owner != this.owner)
        {
            float xDir = this.transform.parent.GetComponent<Rigidbody2D>().velocity.x;
            if (xDir == 0)
                xDir = 1;
            else
                xDir = xDir / Mathf.Abs(xDir);

            Vector2 appliedKnockbackVector = new Vector2(knockbackVector.x * xDir, knockbackVector.y);

            hurtbox.TakeDamage(damage);
            hurtbox.TakeHit(hitlag, hitstun, knockbackVector);


            owner.gainMeter(meterGain);
        }
    }
}
