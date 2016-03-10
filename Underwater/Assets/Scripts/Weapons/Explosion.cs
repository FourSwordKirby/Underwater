using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    public float explosionForce;
    public float explosionLength;

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
            hurtbox.TakeHit(0, 0, launchDirection * explosionForce);
        }

        DestroyableWall wall = col.gameObject.GetComponent<DestroyableWall>();
        if(wall != null)
        {
            Destroy(wall.gameObject);
        }
    }
}
