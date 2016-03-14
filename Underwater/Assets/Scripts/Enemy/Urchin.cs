using UnityEngine;
using System.Collections;

public class Urchin : Enemy {

    /*used for controlling how the squid moves etc.*/
    public StateMachine<Urchin> ActionFsm { get; private set; }

    /*self refernces*/
    public Collider2D attackRange;
    public UrchinSpikeHitbox ammo;


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

    override public void Freeze()
    {
        base.Freeze();
        ActionFsm.SuspendState(new UrchinFrozenState(this, this.ActionFsm));
    }

    override public void Unfreeze()
    {
        base.Unfreeze();
        ActionFsm.ResumeState();
    }
}