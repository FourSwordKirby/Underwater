using UnityEngine;
using System.Collections;

public class Jellyfish : Enemy {

    public float startingHeight { get; private set; }

    private float jumpHeight = 1.0f;

    void Start()
    {
        initBaseClass();

        this.startingHeight = this.transform.position.y;
    }

    void Update()
    {
        this.anim.SetFloat("Direction", Parameters.GetDirAnimation(this.direction));

        this.StatusFsm.Execute();

        if (this.transform.position.y <= this.startingHeight)
            this.selfBody.velocity = new Vector2(0, jumpHeight);

        this.health = Mathf.Clamp(health, 0, maxHealth);
    }
}
