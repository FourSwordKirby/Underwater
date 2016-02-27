using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Hitbox : Collisionbox{
    public float damage;
    public Vector2 knockbackVector;
    public float hitstun;

    //This is mostly for aesthetic stuff
    public Parameters.Effect effect;
}
