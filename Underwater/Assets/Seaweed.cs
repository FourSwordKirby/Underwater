using UnityEngine;
using System.Collections;

public class Seaweed : MonoBehaviour {

    public float health;

    public bool retracted;
    public float recoveryTime;
    public float recoveryTimer;

    public bool frozen;
    public float frozenRecoveryTime;
    public float frozenTimer;

    /// <summary>
    /// This is how much objects which touch the kelp are pushed back
    /// </summary>
    public float pushback;

    private Vector3 initialScale;

    //Used for the initialization of internal, non-object variables
    void Awake()
    {
        this.initialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen)
        {
            frozenTimer += Time.deltaTime;
            if (frozenTimer > frozenRecoveryTime)
            {
                frozenTimer = 0.0f;
            }
            return;
        }

        if (retracted)
        {
            recoveryTimer += Time.deltaTime;
            if (recoveryTimer > recoveryTime)
            {
                recoveryTimer = 0.0f;
            }
        }
    }

    void FixedUpdate()
    {
        if(frozen)
            return;
        if (retracted)
        {
            float xScale = Mathf.Lerp(this.transform.localScale.x, 0.01f, Time.deltaTime);
            float yScale = Mathf.Lerp(this.transform.localScale.y, 0.01f, Time.deltaTime);
            float zScale = Mathf.Lerp(this.transform.localScale.z, 0.01f, Time.deltaTime);
            this.transform.localScale = new Vector3(xScale, yScale, zScale);
        }
        else
        {
            float xScale = Mathf.Lerp(this.transform.localScale.x, initialScale.x, Time.deltaTime);
            float yScale = Mathf.Lerp(this.transform.localScale.y, initialScale.y, Time.deltaTime);
            float zScale = Mathf.Lerp(this.transform.localScale.z, initialScale.z, Time.deltaTime);
            this.transform.localScale = new Vector3(xScale, yScale, zScale);
        }

            //do some stuff;
    }

    public void Freeze()
    {
        this.frozen = true;
    }

    public void Unfreeze()
    {
        this.frozen = false;
    }

    public void Retract()
    {
        this.retracted = true;
    }

    public void Unretract()
    {
        this.retracted = false;
    }
}
