using UnityEngine;
using System.Collections;

public class Bubble : Hitbox {
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

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        /*do stuff with hitting enemies please
        Hurtbox hurtbox = col.gameObject.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            hurtbox.TakeDamage(damage);
            hurtbox.TakeHit(0, hitstun, knockbackVector);

            Destroy(this.gameObject);
        }*/
    }
}
