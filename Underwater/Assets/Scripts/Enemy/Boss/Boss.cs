using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour {

    public int maxHealth;
    private int health;

    public StateMachine<Boss> ActionFsm { get; private set; }


    //self references to various components
    //private Collider selfCollider;
    public Animator anim { get; private set; }
    public Rigidbody2D selfBody { get; private set; }
    public CollisionboxManager hitboxManager { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Collider2D environmentCollisionBox;
    public GameObject squidPrfab;

	// Use this for initialization
	void Start () {
        health = maxHealth;

        if (this.health <= 0)
            Die();
	}
	
	// Update is called once per frame
	void Update () {
        ActionFsm.Execute();
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
    }
}
