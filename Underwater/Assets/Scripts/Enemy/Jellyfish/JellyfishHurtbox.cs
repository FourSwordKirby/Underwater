using UnityEngine;
using System.Collections;

public class JellyfishHurtbox : MonoBehaviour
{
    public Jellyfish owner;

    void OnTriggerEnter2D(Collider2D col)
    {
        //Basic normal bubble stuff
        Bubble damageBubble = col.gameObject.GetComponent<Bubble>();
        if (damageBubble != null)
        {
            if (damageBubble.effect == Parameters.DamageEffect.Freeze)
            {
                owner.TakeDamage(damageBubble.damage);
                if (owner.health < owner.maxHealth * 0.5f && owner.health + damageBubble.damage > owner.maxHealth * 0.5f)
                {
                    owner.Freeze();
                }

                Destroy(col.gameObject);
            }
            else
            {
                AudioSource.PlayClipAtPoint(owner.audio[1], transform.position);
            }
        }
    }
}
