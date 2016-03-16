using UnityEngine;
using System.Collections;

public class Explosion : Hitbox {
    public float explosionForce;
    public float explosionLength;

    public AudioSource explosionSound;

    void Start()
    {
        Camera.main.GetComponent<CameraControls>().Shake(0.1f);
        AudioSource.PlayClipAtPoint(explosionSound.clip, this.transform.position);
    }

    void Update()
    {
        explosionLength -= Time.deltaTime;
        if (explosionLength < 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Hurtbox hurtbox = col.gameObject.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            Vector3 hurtboxPosition = hurtbox.transform.position;
            Vector3 launchDirection = hurtboxPosition - this.transform.position;

            hurtbox.TakeDamage(damage);
            hurtbox.TakeHit(0, 0, launchDirection * explosionForce);
            hurtbox.ApplyEffect(this.effect);
        }
    }
}
