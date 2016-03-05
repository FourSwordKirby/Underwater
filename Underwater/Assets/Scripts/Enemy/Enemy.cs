﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Mobile {

    public float maxHealth;
    public float healthRegenRate;

    public float health;

    public float baseSpeed;
    public float baseDrift;

    public Parameters.Direction direction; //{ get; set; }

    /*used for controlling how the enemy moves etc.*/
    public StateMachine<Enemy> ActionFsm { get; private set; }

    /*used for things like health regen, which is seperate from attacking*/
    public StateMachine<Enemy> StatusFsm { get; private set; }

    //self references to various components
    //private Collider selfCollider;
    public Animator anim { get; private set; }
    public Rigidbody2D selfBody { get; private set; }
    public CollisionboxManager hitboxManager { get; private set; }
    public ECB environmentCollisionBox;
    public List<GameObject> prefabs;
    /*private GameObject bodyVisual;
    public PlayerSounds Sounds { get; private set; }
    */

    //Used for the initialization of internal, non-object variables
    void Awake()
    {
        health = maxHealth;
    }

    // Use this for initialization of variables that rely on other objects
	void Start () {
        //Initializing components
        anim = this.GetComponent<Animator>();
        selfBody = this.GetComponent<Rigidbody2D>();
        hitboxManager = this.GetComponent<CollisionboxManager>();


        StatusFsm = new StateMachine<Enemy>(this);
        State<Enemy> startState = new DefaultState(this, this.ActionFsm);
        StatusFsm.InitialState(startState);

        /*
        ActionFsm = new StateMachine<Player>(this);
        State<Enemy> startState = new IdleState(this, this.ActionFsm);
        ActionFsm.InitialState(startState);
         * */
    }
	
	// Update is called once per frame
	void Update () {
        this.anim.SetFloat("Direction", Parameters.GetDirAnimation(this.direction));

        //this.ActionFsm.Execute();
        this.StatusFsm.Execute();

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    void FixedUpdate()
    {
        //this.ActionFsm.FixedExecute();
        this.StatusFsm.FixedExecute();
    }
}
