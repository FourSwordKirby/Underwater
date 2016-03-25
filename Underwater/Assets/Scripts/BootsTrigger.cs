using UnityEngine;
using System.Collections;

public class BootsTrigger : MonoBehaviour {

    public Collider2D hiddenWall;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerHurtbox>() != null)
        {
            hiddenWall.isTrigger = true;
        }
    }
}
