using UnityEngine;
using System.Collections;

public class SquidEnemy : Enemy {

    public float startingHeight {get; private set;}

    /*used for controlling how the squid moves etc.*/
    public StateMachine<SquidEnemy> ActionFsm { get; private set; }

    public GameObject currentTarget;


    /*self refernces*/
    public Collider2D attackRange;

	// Use this for initialization
	void Start () {
        initBaseClass();

        this.startingHeight = this.transform.position.y;
    
        ActionFsm = new StateMachine<SquidEnemy>(this);
        State<SquidEnemy> startState = new SquidIdleState(this, this.ActionFsm);
        ActionFsm.InitialState(startState);
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
