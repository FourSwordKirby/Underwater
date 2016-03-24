using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour {

    public AudioClip audio;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            if (audio != null)
            {
                AudioSource.PlayClipAtPoint(audio, this.transform.position);
                audio = null;
            }
        }
    }
}
