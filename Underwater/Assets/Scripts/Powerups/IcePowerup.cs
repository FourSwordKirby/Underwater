using UnityEngine;
using System.Collections;

public class IcePowerup : MonoBehaviour {

    public Weapon IceBubbler;

    void OnTriggerEnter2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner; 
            player.AddWeapon(IceBubbler);

            //Make the player go into a dialog state + cutscenes I guess
            Destroy(this.gameObject);
        }
    }
}
