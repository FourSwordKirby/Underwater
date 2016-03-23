using UnityEngine;
using System.Collections;

public class BackgroundTransition : MonoBehaviour {

    public SpriteRenderer background;
    public bool setting;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            background.enabled = setting;
        }
    }
}
