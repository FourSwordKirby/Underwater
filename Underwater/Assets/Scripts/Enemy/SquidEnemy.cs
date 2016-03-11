using UnityEngine;
using System.Collections;

public class SquidEnemy : Enemy {

    /*used for controlling how the squid moves etc.*/
    public StateMachine<SquidEnemy> ActionFsm { get; private set; }

    public GameObject currentTarget;

    /*self refernces*/
    public Collider2D attackRange;

	// Use this for initialization
	void Start () {
        initBaseClass();

        ActionFsm = new StateMachine<SquidEnemy>(this);
        //State<SquidEnemy> startState = new DefaultState(this, this.StatusFsm);
        //ActionFsm.InitialState(startState);
	}
	
	// Update is called once per frame
	void Update () {
        ActionFsm.Execute();
	}

    void FixedUpdate()
    {
        ActionFsm.FixedExecute();
    }
}
