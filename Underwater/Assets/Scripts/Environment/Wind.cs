using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

    public float strength;
    public Vector2 force;

    public float activePeriod;
    public float cooldownPeriod;
    private float timer;

    /*self-references*/
    public Collider2D windArea;
    public SpriteRenderer spriteRenderer;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (windArea.enabled && timer > activePeriod)
        {
            timer = 0.0f;
            windArea.enabled = false;
            spriteRenderer.enabled = false;
        }
        else if (!windArea.enabled && timer > cooldownPeriod)
        {
            timer = 0.0f;
            windArea.enabled = true;
            spriteRenderer.enabled = true;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            hurtbox.owner.ApplyPushForce(this.force);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        PlayerHurtbox hurtbox = col.gameObject.GetComponent<PlayerHurtbox>();
        if (hurtbox != null)
        {
            hurtbox.owner.ApplyPushForce(this.force);
        }

        if (col.gameObject.GetComponent<Bomb>() != null)
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity += this.force;
        }
    }
}
