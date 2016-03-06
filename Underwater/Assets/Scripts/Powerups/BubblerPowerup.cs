using UnityEngine;
using System.Collections;

public class BubblerPowerup : MonoBehaviour
{

    public Weapon BasicBubbler;

    void OnTriggerEnter2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner;
            //Make the player go into a dialog state
            player.AddWeapon(BasicBubbler);

            //Make the player go into a dialog state + cutscenes I guess
            Destroy(this.gameObject);
        }
    }
}
