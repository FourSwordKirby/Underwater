using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Urchin : Enemy {

    /*used for controlling how the squid moves etc.*/
    public StateMachine<Urchin> ActionFsm { get; private set; }

    /*self refernces*/
    public Collider2D attackRange;
    public UrchinSpikeHitbox ammo;
    public List<AudioClip> audio;

    // Use this for initialization
    void Start()
    {
        initBaseClass();

        ActionFsm = new StateMachine<Urchin>(this);
        State<Urchin> startState = new UrchinIdleState(this, this.ActionFsm);
        ActionFsm.InitialState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        //this.anim.SetFloat("Direction", Parameters.GetDirAnimation(this.direction));

        ActionFsm.Execute();
        StatusFsm.Execute();

        if (frozen)
        {
            spriteRenderer.color = new Color(0.25f, 1f, 1f, 1f);
        }
        else
        {
            spriteRenderer.color = new Color(0.8f + 0.2f * (health / maxHealth),
                                            0.36f + 0.64f * (health / maxHealth),
                                            0.36f + 0.64f * (health / maxHealth), 1f);
        }

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        ActionFsm.FixedExecute();
        StatusFsm.Execute();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        AudioSource.PlayClipAtPoint(audio[1], transform.position);
    }

    override public void Freeze()
    {
        base.Freeze();
        ActionFsm.SuspendState(new UrchinFrozenState(this, this.ActionFsm));
    }

    override public void Unfreeze()
    {
        base.Unfreeze();
        ActionFsm.ResumeState();
        this.selfBody.isKinematic = true;
    }
}