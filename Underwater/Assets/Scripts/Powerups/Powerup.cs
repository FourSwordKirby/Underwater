using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
    private float pickupDespawnLength = 2.0f;
    private float timer;

    /*self references*/
    public SpriteRenderer sprite;
    public Collider2D triggerBox;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                Destroy(this.gameObject);
        }
    }

    public void finishPickup()
    {
        sprite.enabled = false;
        triggerBox.enabled = false;
        timer = pickupDespawnLength;
    }
}
