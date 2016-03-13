using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
    void OnTriggerStay2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null && hurtbox.owner != null)
        {
            hurtbox.owner.isUnderWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null && hurtbox.owner != null)
        {
            hurtbox.owner.isUnderWater = false;
        }
    }
}
