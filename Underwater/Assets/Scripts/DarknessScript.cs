using UnityEngine;
using System.Collections;

public class DarknessScript : MonoBehaviour {

    public float transparency = 1.0f;
    private bool entered;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (entered)
        {
            if (transparency > 0.0)
            {
                transparency -= 0.02f;
                Color oldColor = GetComponent<SpriteRenderer>().color;
                Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, transparency);
                GetComponent<SpriteRenderer>().color = newColor;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<PlayerHurtbox>() != null)
        {
            entered = true;
        }
    }
}
