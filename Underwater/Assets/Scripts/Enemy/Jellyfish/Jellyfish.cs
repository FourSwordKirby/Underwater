using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jellyfish : Enemy {

    public float startingHeight { get; private set; }
    private float jumpHeight = 1.0f;

    /*self refernces*/
    public List<AudioClip> audio;

    void Start()
    {
        initBaseClass();

        this.startingHeight = this.transform.position.y;

    }

    void Update()
    {
        this.anim.SetFloat("Direction", Parameters.GetDirAnimation(this.direction));

        this.StatusFsm.Execute();

        if (this.transform.position.y <= this.startingHeight)
            this.selfBody.velocity = new Vector2(0, jumpHeight);

        if (frozen)
        {
            spriteRenderer.color = new Color(0.25f, 1f, 1f, 1f);
        }
        else
        {
            spriteRenderer.color = new Color(0.1f + 0.9f * (health / maxHealth), 1, 1, 1);
        }

        this.health = Mathf.Clamp(health, 0, maxHealth);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        AudioSource.PlayClipAtPoint(audio[0], transform.position);
    }

    override public void Freeze()
    {
        base.Freeze();
        this.selfBody.velocity = Vector2.zero;
    }

    override public void Unfreeze()
    {
        base.Unfreeze();
        this.selfBody.gravityScale = 0.1f;
    }
}
