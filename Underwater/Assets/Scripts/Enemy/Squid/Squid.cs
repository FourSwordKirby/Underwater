using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squid : Enemy {

    public float startingHeight {get; private set;}

    /*used for controlling how the squid moves etc.*/
    public StateMachine<Squid> ActionFsm { get; private set; }

    public GameObject currentTarget;

    /*self refernces*/
    public Collider2D attackRange;
    public List<AudioClip> audio;

	// Use this for initialization
	void Start () {
        initBaseClass();

        this.startingHeight = this.transform.position.y;
    
        ActionFsm = new StateMachine<Squid>(this);
        State<Squid> startState = new SquidIdleState(this, this.ActionFsm);
        ActionFsm.InitialState(startState);
	}
	
	// Update is called once per frame
	void Update () {
        this.anim.SetFloat("Direction", Parameters.GetDirAnimation(this.direction));

        ActionFsm.Execute();
        StatusFsm.Execute();

        if (frozen)
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            Color newColor = Color.white;
            newColor.r = 200 + 55 * (health / maxHealth);
            newColor.r = 90 + 165 * (health / maxHealth);
            newColor.r = 90 + 165 * (health / maxHealth);
            spriteRenderer.color = newColor;
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
        AudioSource.PlayClipAtPoint(audio[3], transform.position);
    }

    override public void Freeze()
    {
        base.Freeze();
        ActionFsm.ChangeState(new SquidFrozenState(this, this.ActionFsm));
        this.attackRange.gameObject.SetActive(false);
    }

    override public void Unfreeze()
    {
        base.Unfreeze();
        this.attackRange.gameObject.SetActive(true);
        ActionFsm.ChangeState(new SquidIdleState(this, this.ActionFsm));
    }
}
