using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        Hurtbox hurtbox = col.gameObject.GetComponent<Hurtbox>();
        if (hurtbox != null && hurtbox.owner != null)
        {
            hurtbox.owner.isUnderWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Hurtbox hurtbox = col.gameObject.GetComponent<Hurtbox>();
        if (hurtbox != null && hurtbox.owner != null)
        {
            hurtbox.owner.isUnderWater = false;
        }
    }
}
