using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

    public float strength;
    public Vector2 force;

    public float activePeriod;
    public float cooldownPeriod;
    private float timer;

    /*self-references*/
    public Collider2D windArea;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (windArea.enabled && timer > activePeriod)
        {
            timer = 0.0f;
            windArea.enabled = false;
        }
        else if (!windArea.enabled && timer > cooldownPeriod)
        {
            timer = 0.0f;
            windArea.enabled = true;
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null)
        {
            hurtbox.owner.ApplyPushForce(this.force);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        TestHurtbox hurtbox = col.gameObject.GetComponent<TestHurtbox>();
        if (hurtbox != null)
        {
            hurtbox.owner.ApplyPushForce(this.force);
        }
    }
}
