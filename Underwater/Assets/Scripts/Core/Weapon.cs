using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

    public string name;

    /// <summary>
    /// What happens when the weapon fires
    /// You need to specify what direction the weapon is being fired in.
    /// </summary>
    public abstract void Fire(Parameters.Direction dir, Parameters.Aim aim);

    /// <summary>
    /// What happens when the player releases the fire button
    /// </summary>
    public abstract void CeaseFire();


    /// <summary>
    /// This will get the amount of ammo remaining in the gun
    /// For weapons with unlimited ammo, it will return a -1;
    /// </summary>
    /// <returns>returns the amount of ammo remaining in the gun. If the ammo is unlimited, returns -1</returns>
    public abstract int GetAmmoCount();
}
