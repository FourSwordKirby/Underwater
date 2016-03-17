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
    public int currentWeaponIndex;

    public Interactable currentInteractable;

    public float baseMovementSpeed;
    public float baseMovementDrift;
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
                return 0.75f * baseMovementSpeed;
            else
                return baseMovementSpeed;
        }
    }
    public float movementDrift
    {
        get
        {
            if (isUnderWater)
                return 0.5f * baseMovementDrift;
            else
                return baseMovementDrift;
        }
    }
    public float friction;
    public float jumpHeight
    {
        get
        {
            if(!isUnderWater)
                return baseJumpHeight;
            else if(isWeighted)
                return 1.2f * baseJumpHeight;
            else
                return 2.0f * baseJumpHeight;
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
                return 0.75f * baseAirMovementSpeed;
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

    //Booleans for items
    public bool hasWeights;

    //Stateful things
    public bool isUnderWater;
    public bool grounded;
    public bool isWeighted;
    public Vector2 externalForce;

    private float baseInvulnTime;
    public float invulnTime;

    public Parameters.Direction direction; //{ get; set; }
    public Parameters.Aim aim; //{ get; set; }

    private bool lockedDir;
    private bool inCutscene;

    //Tells us the status of the player (things that affect the hitbox)
    public Parameters.PlayerStatus status {get; set; }

    public StateMachine<Player> ActionFsm { get; private set; }

    //self references to various components
    //private Collider selfCollider;
    public Animator anim { get; private set; }
    public Rigidbody2D selfBody { get; private set; }
    public CollisionboxManager hitboxManager { get; private set; }
    public ECB environmentCollisionBox;
    public DialogBox dialogBox;
    public List<AudioClip> audio;
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
    void Update()
    {
        this.ActionFsm.Execute();

        if (invulnTime > 0)
        {
            invulnTime -= Time.deltaTime;

            if (invulnTime <= 0)
            {
                this.environmentCollisionBox.gameObject.layer = LayerMask.NameToLayer("Player");
                this.hitboxManager.activateAllHitboxes();
            }
        }

        if (inCutscene)
            return;
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

        //Shooting controls
        if (Controls.ShootInputHeld() && activeWeapon != null)
        {
            if (isUnderWater)
                activeWeapon.Fire(direction, aim);
            else
            {
                AudioSource.PlayClipAtPoint(audio[0], transform.position);
                //Do another indicator you can't fire
            }
            LockDirection();
        }
        else
        {
            activeWeapon.CeaseFire();
            if (grounded)
                UnlockDirection();
        }

        //Switching Weapons Controls
        if(Controls.PrevWeaponInputDown())
        {
            AudioSource.PlayClipAtPoint(audio[1], transform.position);
            SwitchWeapons(currentWeaponIndex - 1);
        }
        if (Controls.NextWeaponInputDown())
        {
            AudioSource.PlayClipAtPoint(audio[1], transform.position);
            SwitchWeapons(currentWeaponIndex + 1);
        }

        //Interacting Controls
        if (Controls.InteractInputDown() && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }

        //Toggle controls (for weights and related things)
        if (hasWeights && Controls.Toggle1Down())
        {
            if(isWeighted)
                AudioSource.PlayClipAtPoint(audio[3], transform.position);
            else
                AudioSource.PlayClipAtPoint(audio[2], transform.position);

            isWeighted = !isWeighted;
            Debug.Log("Weights Equipped");
        }
    }

    void FixedUpdate()
    {
        this.ActionFsm.FixedExecute();

        //Hacky stuff to deal with wind and related pushing based things
        if(this.selfBody.velocity.magnitude < externalForce.magnitude + movementSpeed)
            this.selfBody.velocity += externalForce;
        externalForce = Vector2.zero;
    }

    public void LoseHealth(float damage)
    {
        if (damage > 0)
            this.health -= damage;
        if (this.health < 0)
            Debug.Log("dead player");
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

    public void LockControls()
    {
        inCutscene = true;
        foreach(Weapon weapon in weaponInventory)
            weapon.CeaseFire();
        this.selfBody.velocity = new Vector2(0.0f, this.selfBody.velocity.y);
    }

    public void UnlockControls()
    {
        inCutscene = false;
    }

    public void AddWeapon(Weapon weaponPrefab)
    {
        Weapon weapon = Instantiate(weaponPrefab);
        weapon.transform.parent = this.gameObject.transform.FindChild("Weapons");
        weapon.transform.position = this.gameObject.transform.position;

        this.activeWeapon = weapon;
        this.weaponInventory.Add(weapon);
        this.currentWeaponIndex = weaponInventory.Count - 1;
    }

    public void SwitchWeapons(int weaponIndex)
    {
        if (weaponInventory.Count == 0)
            return;

        if (weaponIndex > weaponInventory.Count - 1)
        {
            weaponIndex = 0;
        }
        if (weaponIndex < 0)
        {
            weaponIndex = weaponInventory.Count - 1;
        }
        this.currentWeaponIndex = weaponIndex;
        this.activeWeapon = weaponInventory[currentWeaponIndex];
    }

    public void ApplyPushForce(Vector2 force)
    {
        if(!isWeighted)
            externalForce = force;
    }
}
