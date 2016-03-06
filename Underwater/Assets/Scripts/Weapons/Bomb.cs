using UnityEngine;
using System.Collections;

public class Bomb : Hitbox {
    public float fuseLength;

    /*self references*/
    public Rigidbody2D selfBody;

    void Update()
    {
        fuseLength -= Time.deltaTime;
        if (fuseLength < 0)
        {
            Destroy(this.gameObject);

            //SPAWN A COOL EXPLOSION
        }
    }
}
