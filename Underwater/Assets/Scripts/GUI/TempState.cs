using UnityEngine;
using System.Collections;

public class TempState : MonoBehaviour {

	public static TempState Instance = null;

	public string char1 = "";

	void Awake () {
		// Checks for conflicting instances
		if (Instance == null) {
			Instance = this;
		}

		DontDestroyOnLoad (gameObject);
	}

	public void setChar1 (string name) {
		char1 = name;
		print(char1);
	}

	public string getChar1 () {
		return char1;
	}
}
