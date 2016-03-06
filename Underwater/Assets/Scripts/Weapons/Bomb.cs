using UnityEngine;
using System.Collections;

public class Bomb : Hitbox {
    public float fuseLength;

    public Explosion explosionPrefab;

    /*self references*/
    public Rigidbody2D selfBody;

    void Update()
    {
        fuseLength -= Time.deltaTime;
        if (fuseLength < 0)
        {
            //SPAWN A COOL EXPLOSION
            Explosion explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.position = this.gameObject.transform.position;

            Destroy(this.gameObject);
        }
    }
}
