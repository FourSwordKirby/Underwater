using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Mobile {

    public float maxHealth;
    public float maxJetpackFuel;    //Amount of time the jetpack can be used.


    public float health { get; private set; }
    public float jetpackFuel { get; private set; }

    public Weapon activeWeapon;
    public List<Weapon> weaponInventory;

    public float baseMovementSpeed;
    public float baseFriction;
    public float baseJumpHeight;
    public float baseFallSpeed;
    public float baseAirMovementSpeed;
    public float baseAirDrift;
    public float baseJetpackSpeed;


    public float movementSpeed
    {
        get
        {
            if(isUnderWater)
                return 0.5f * baseMovementSpeed;
            else
                return baseMovementSpeed;
        }
    }
    public float friction;
    public float jumpHeight
    {
        get
        {
            if (isUnderWater)
                return 2.0f * baseJumpHeight;
            else
                return baseJumpHeight;
        }
    }
    public float fallSpeed
    {
        get
        {
            if (isUnderWater)
                return 0.5f * fallSpeed;
            else
                return fallSpeed;
        }
    }
    public float airMovementSpeed
    {
        get
        {
            if (isUnderWater)
                return 0.5f * baseAirMovementSpeed;
            else
                return baseAirMovementSpeed;
        }
    }
    public float airDrift
    {
        get
        {
            if (isUnderWater)
                return 0.5f * baseAirDrift;
            else
                return baseAirDrift;
        }
    }
    public float jetpackSpeed
    {
        get
        {
            if(isUnderWater)
                return baseJetpackSpeed;
            else
                return baseJetpackSpeed/4;
        }
    }

    public bool isUnderWater;
    public bool grounded;

    public Parameters.Direction direction; //{ get; set; }
    public Parameters.Aim aim; //{ get; set; }

    private bool lockedDir;

    //Tells us the status of the player (things that affect the hitbox)
    public Parameters.PlayerStatus status {get; set; }

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
        health = maxHealth;
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
        //Animation control stuff
        Vector2 movementInputVector = Controls.getDirection();

        //Controlling where we are aiming
        this.aim = Parameters.VectorToAim(movementInputVector);
        if (this.grounded && this.aim == Parameters.Aim.Down)
            this.aim = Parameters.Aim.TiltDown;
        if (Controls.AimDownInputHeld())
        {
            this.aim = Parameters.Aim.TiltDown;
        }
        if (Controls.AimUpInputHeld())
        {
            this.aim = Parameters.Aim.TiltUp;
        }


        //Controlling the direction we are facing
        if (movementInputVector.x != 0 && !lockedDir)
            this.direction = Parameters.VectorToDir(movementInputVector);

        this.anim.SetFloat("Direction", Parameters.GetDirAnimation(this.direction));
        this.anim.SetFloat("Aim", Parameters.GetAimAnimation(this.aim));

        this.ActionFsm.Execute();

        //Testing of the other buttons
        if (Controls.ShootInputHeld())
        {
            activeWeapon.Fire(direction, aim);
            LockDirection();
        }
        else
        {
            activeWeapon.CeaseFire();
            if(grounded)
                UnlockDirection();
        }
	}

    void FixedUpdate()
    {
        this.ActionFsm.FixedExecute();
    }

    public void LoseHealth(float damage)
    {
        if (damage > 0)
            this.health -= damage;
    }

    public void RegainHealth(float amount)
    {
        this.health = Mathf.Clamp(this.health + amount, 0.0f, maxHealth);
    }

    public void UseFuel(float amount)
    {
        this.jetpackFuel -= amount;
    }

    public void RefillFuel()
    {
        this.jetpackFuel = maxJetpackFuel;
    }

    public void Die()
    {
        Debug.Log("died");
    }

    public void LockDirection()
    {
        lockedDir = true;
    }

    public void UnlockDirection()
    {
        lockedDir = false;
    }
}
