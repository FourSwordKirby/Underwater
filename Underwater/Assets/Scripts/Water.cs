using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null && hurtbox.owner != null)
        {
            hurtbox.owner.isUnderWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null && hurtbox.owner != null)
        {
            hurtbox.owner.isUnderWater = false;
        }
    }
}
