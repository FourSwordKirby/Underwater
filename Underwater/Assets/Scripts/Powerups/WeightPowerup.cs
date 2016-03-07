using UnityEngine;
using System.Collections;

public class WeightPowerup : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null)
        {
            Player player = hurtbox.owner;
            player.hasWeights = true;

            //Make the player go into a dialog state + cutscenes I guess
            Destroy(this.gameObject);
        }
    }
}
