using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Mobile {

    public float maxHealth;

    public float health { get; private set; }

    public float movementSpeed;
    public float friction;
    public float jumpHeight;
    public float fallSpeed;
    public float airMovementSpeed;

    public bool isUnderWater;

    public bool grounded;

    public float jetpackFuel;

    public Parameters.InputDirection direction { get; set; }
    
    //Tells us the status of the player (things that affect the hitbox)
    public Parameters.PlayerStatus status {get; set; }
    
    public const int DEFAULT_MAX_HEALTH = 100;
    public const int DEFAULT_MAX_METER = 100;

    public const float DEFAULT_SPEED = 2.0f;
    public const float DEFAULT_FRICTION = 1.0f;
    public const float DEFAULT_JUMP_HEIGHT = 10.0f;
    public const float DEFAULT_FALL_SPEED = 1.0f;
    public const float DEFAULT_AIR_MOVEMENT_SPEED = 2.0f;

    public StateMachine<Player> ActionFsm { get; private set; }

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
        maxHealth = DEFAULT_MAX_HEALTH;
        health = maxHealth;

        /*
        baseKnockdownThreshold = DEFAULT_KNOCKDOWN_THRESHOLD;
        knockdownThreshold = baseKnockdownThreshold;

        this.movementSpeed = DEFAULT_SPEED;
        this.rollSpeed = DEFAULT_ROLL_SPEED;
        this.friction = DEFAULT_FRICTION;
        this.jumpHeight = DEFAULT_JUMP_HEIGHT;
        this.fallSpeed = DEFAULT_FALL_SPEED;
        this.airMovementSpeed = DEFAULT_AIR_MOVEMENT_SPEED;
        this.maxAirJumps = DEFAULT_MAX__AIR_JUMPS;
        this.maxAirDashes = DEFAULT_MAX__AIR_DASHES;
         */
    }

    // Use this for initialization of variables that rely on other objects
	void Start () {
        //Initializing components
        anim = this.GetComponent<Animator>();
        selfBody = this.GetComponent<Rigidbody2D>();
        hitboxManager = this.GetComponent<CollisionboxManager>();

        ActionFsm = new StateMachine<Player>(this);
        State<Player> startState = new IdleState(this, this.ActionFsm);
        ActionFsm.InitialState(startState);
	}
	
	// Update is called once per frame
	void Update () {
        this.ActionFsm.Execute();

        //Testing of the other buttons
        if (Controls.attackInputDown(this))
        {
            GameObject newFireball = Instantiate(prefabs[0]);
            newFireball.GetComponentInChildren<FireballHitbox>().owner = this;
            newFireball.transform.position = this.transform.position + new Vector3(0, 1, 0);
            float xDir = Parameters.getVector(direction).x;
            newFireball.GetComponent<Rigidbody2D>().velocity = new Vector3(xDir * 4, 0, 0);
        }
	}

    void FixedUpdate()
    {
        this.ActionFsm.FixedExecute();
    }

    public void loseHealth(float damage)
    {
        if (damage > 0)
            this.health -= damage;
    }

    public void Die()
    {
        Debug.Log("died");
    }
}
