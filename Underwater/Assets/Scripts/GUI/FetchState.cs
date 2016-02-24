using UnityEngine;
using System.Collections;

public class FetchState : MonoBehaviour {

	public GameObject tempStateHolder; 
	public TempState tempState;

	// Use this for initialization
	void Start () {
		tempStateHolder = GameObject.Find ("TempState");
		tempState = tempStateHolder.GetComponent<TempState> ();
		if (tempState) {
			print ("NEW SCENE LOAD : " + tempState.char1);
		}
	}
}
