using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Mobile {

    public float maxHealth;
    public float baseHealthRegenRate;

    public float health;

    public bool frozen;
    public float healthRegenRate
    {
        get
        {
            if (frozen)
                return 0.5f * baseHealthRegenRate;
            else
                return baseHealthRegenRate;
        }
    }

    public Parameters.Direction direction;

    /*used for things like health regen, which is seperate from attacking
     This is universal to all enemies, so we put it in this base class*/
    public StateMachine<Enemy> StatusFsm { get; private set; }

    //self references to various components
    //private Collider selfCollider;
    public Animator anim { get; private set; }
    public Rigidbody2D selfBody { get; private set; }
    public CollisionboxManager hitboxManager { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Collider2D environmentCollisionBox;
    public List<GameObject> prefabs;
    /*private GameObject bodyVisual;
    public PlayerSounds Sounds { get; private set; }
    */

    //Used for the initialization of internal, non-object variables
    void Awake()
    {
        health = maxHealth;
        initBaseClass();
    }

    // Use this for initialization of variables that rely on other objects
	void Start () {
    }

    public void initBaseClass()
    {
        //Initializing components
        anim = this.GetComponent<Animator>();
        selfBody = this.GetComponent<Rigidbody2D>();
        hitboxManager = this.GetComponent<CollisionboxManager>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        StatusFsm = new StateMachine<Enemy>(this);
        State<Enemy> startState = new DefaultState(this, this.StatusFsm);
        StatusFsm.InitialState(startState);
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

    public virtual void TakeDamage(float damage)
    {
        this.health -= damage;
    }

    public virtual void Freeze()
    {
        this.frozen = true;

        this.gameObject.layer = LayerMask.NameToLayer("Platform");
        this.environmentCollisionBox.gameObject.layer = LayerMask.NameToLayer("Platform");
        this.selfBody.isKinematic = true;
        this.selfBody.gravityScale = 0;

        hitboxManager.deactivateAllHitboxes();
    }

    public virtual void Unfreeze()
    {
        this.frozen = false;

        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.environmentCollisionBox.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.selfBody.isKinematic = false;

        hitboxManager.activateAllHitboxes();
    }
}
