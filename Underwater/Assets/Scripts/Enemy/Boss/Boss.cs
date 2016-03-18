using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour {

    public int maxHealth;
    public int health;
    public bool injured { get; private set; }

    public StateMachine<Boss> ActionFsm { get; private set; }

    //graphical stuff
    private Color previousColor;
    private Color hitColor = Color.red;
    private float hitFlashLength = 0.2f;
    private float timer;


    //self references to various components
    //private Collider selfCollider;
    public Animator anim { get; private set; }
    public Rigidbody2D selfBody { get; private set; }
    public CollisionboxManager hitboxManager { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Collider2D environmentCollisionBox;


    public GameObject mouthPosition;
    public Wind inhaleHitbox;
    public List<GameObject> spawnableEnemies;

    public enum BossStates
    {
        Swipe,
        Suction,
        Summon
    }

    void Awake()
    {
        //Initializing components
        anim = this.GetComponent<Animator>();
        selfBody = this.GetComponent<Rigidbody2D>();
        hitboxManager = this.GetComponent<CollisionboxManager>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
        health = maxHealth;

        ActionFsm = new StateMachine<Boss>(this);
        State<Boss> startState = new BossSummonState(this, this.ActionFsm, 2);//new BossIdleState(this, this.ActionFsm, BossStates.Swipe);
        ActionFsm.InitialState(startState);

        if (this.health <= 0)
            Die();
	}
	
	// Update is called once per frame
	void Update () {
        ActionFsm.Execute();

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            this.spriteRenderer.color = hitColor;
            if (timer <= 0)
                this.spriteRenderer.color = previousColor;
        }
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        ActionFsm.FixedExecute();
    }


    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.spriteRenderer.color != hitColor)
            previousColor = this.spriteRenderer.color;
        timer = hitFlashLength;
    }
}
