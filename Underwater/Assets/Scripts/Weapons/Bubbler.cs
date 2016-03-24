using UnityEngine;
using System.Collections;

public class Bubbler : Weapon {

    public int maxAmmoCount;
    private int currentAmmoCount;

    public Bubble ammo;

    /// <summary>
    /// This is the recharge rate for the bubbler in bubbles per second
    /// </summary>
    public int rechargeRate;

    /// <summary>
    /// This is the fire rate for the bubbler in bubbles per second
    /// </summary>
    public int fireRate;

    public float bubbleSpeed;

    private bool firing;
    private float fireTimer;
    private float rechargeTimer;

    private float bubbleDistance = 2.0f;

    private Vector2 firePosition;
    private Vector2 fireVelocity;

    /*audio references*/
    public AudioSource firingAudio;

	// Use this for initialization
	void Start () 
    {
        fireTimer = 1.0f / fireRate;
        currentAmmoCount = maxAmmoCount;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (firing && currentAmmoCount > 0)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > 1.0f / fireRate)
            {
                int firedAmmo = (int)(fireRate * fireTimer);
                for (int i = 0; i < firedAmmo; i++)
                {
                    Bubble bubble = Instantiate(ammo);
                    bubble.transform.position = firePosition;
                    bubble.selfBody.velocity = fireVelocity;
                    currentAmmoCount--;

                    firingAudio.Play();
                }
                fireTimer = 0.0f;
            }
        }

        if (currentAmmoCount < maxAmmoCount && !firing)
        {
            //Initialize the firing timer;
            fireTimer = 1.0f / fireRate;

            //Recharging!
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer > 1.0f / rechargeRate)
            {
                currentAmmoCount += (int)(rechargeRate * rechargeTimer);
                rechargeTimer = 0.0f;
            }
        }
	}

    override public void Fire(Parameters.Direction dir, Parameters.Aim aim)
    {
        firing = true;

        float xRand = Random.Range(-0.1f, 0.1f);
        float yRand = Random.Range(-0.1f, 0.1f);

        if(dir == Parameters.Direction.Left)
        {
            if (aim == Parameters.Aim.Up)
            {
                firePosition = this.transform.position + new Vector3(0, 0.5f, 0) * bubbleDistance;
                fireVelocity = new Vector3(xRand, 1, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.TiltUp)
            {
                firePosition = this.transform.position + new Vector3(-0.25f, 0.25f, 0) * bubbleDistance;
                fireVelocity = new Vector3(-0.5f + xRand, 0.5f + yRand, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.Neutral)
            {
                firePosition = this.transform.position + new Vector3(-0.5f, 0, 0) * bubbleDistance;
                fireVelocity = new Vector3(-1, yRand, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.TiltDown)
            {
                firePosition = this.transform.position + new Vector3(-0.25f, -0.25f, 0) * bubbleDistance;
                fireVelocity = new Vector3(-0.5f + xRand, -0.5f + yRand, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.Down)
            {
                firePosition = this.transform.position + new Vector3(-0.25f, -0.25f, 0) * bubbleDistance;
                fireVelocity = new Vector3(-0.5f + xRand, -0.5f + yRand, 0) * bubbleSpeed;
                //Temporary measures to mesh sprites together
                /*
                firePosition = this.transform.position + new Vector3(0, -0.5f, 0);
                fireVelocity = new Vector3(xRand, -1, 0) * bubbleSpeed;
                 */
            }
        }

        else if (dir == Parameters.Direction.Right)
        {
            if (aim == Parameters.Aim.Up)
            {
                firePosition = this.transform.position + new Vector3(0, 0.5f, 0) * bubbleDistance;
                fireVelocity = new Vector3(xRand, 1, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.TiltUp)
            {
                firePosition = this.transform.position + new Vector3(0.25f, 0.25f, 0) * bubbleDistance;
                fireVelocity = new Vector3(0.5f + xRand, 0.5f + yRand, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.Neutral)
            {
                firePosition = this.transform.position + new Vector3(0.5f, 0, 0) * bubbleDistance;
                fireVelocity = new Vector3(1, yRand, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.TiltDown)
            {
                firePosition = this.transform.position + new Vector3(0.25f, -0.25f, 0) * bubbleDistance;
                fireVelocity = new Vector3(0.5f + xRand, -0.5f + yRand, 0) * bubbleSpeed;
            }
            else if (aim == Parameters.Aim.Down)
            {
                firePosition = this.transform.position + new Vector3(0, -0.5f, 0) * bubbleDistance;
                fireVelocity = new Vector3(0.5f + xRand, -0.5f + yRand, 0) * bubbleSpeed;
            }
        }
    }

    public override void CeaseFire()
    {
        firing = false;
    }

    override public int GetAmmoCount()
    {
        return currentAmmoCount;
    }
}
