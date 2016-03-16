using UnityEngine;
using System.Collections;

public class UrchinSpikeHitbox : Hitbox {

    public float decayTime;

    /*self references*/
    public Rigidbody2D selfBody;

    void Update()
    {
        decayTime -= Time.deltaTime;
        if (decayTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Basic normal bubble stuff
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            hurtbox.TakeDamage(damage);
            hurtbox.TakeHit(0, hitstun, knockbackVector);
            hurtbox.ApplyEffect(this.effect);

            Destroy(this.gameObject);
        }
    }
}
