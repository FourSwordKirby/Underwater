using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogBox : MonoBehaviour {
    public TextMesh dialogField;
    public float width;

    private string dialog = "";
    private int dialogTracker = 0;

    private float textDisplaySpeed;
    private float textDisplayTimer;

    private const  float FAST_DISPLAY_SPEED = 0.0f;
    private const float SLOW_DISPLAY_SPEED = 0.03f;

    // Use this for initialization
	void Start () {
  	    this.dialogField.text = "";
	}

    void Update () {
        //Do something where text appears according to the textDisplaySpeed
        if (textDisplayTimer> 0)
        {
            textDisplayTimer -= Time.deltaTime;
            return;
        }

        if (this.dialogField.text != dialog)
        {
            if (dialogTracker >= dialog.Length)
                return;
            this.dialogField.text += dialog[dialogTracker];
            dialogTracker++;

            textDisplayTimer = textDisplaySpeed;
        }
	}

    public void forceDialog(string dialog)
    {
        this.dialogField.text = FitText(dialog, width);
    }

    public void displayDialog(string dialog, DisplaySpeed displaySpeed = DisplaySpeed.fast)
    {
        this.gameObject.SetActive(true);
        this.dialog = FitText(dialog, width);
        this.dialogTracker = 0;

        //Prevents the name from flickering
        this.dialogField.text = "";

        if (displaySpeed == DisplaySpeed.immediate)
        {
            this.dialogField.text = dialog;
        }
        else if (displaySpeed == DisplaySpeed.fast)
        {
            textDisplaySpeed = FAST_DISPLAY_SPEED;
        }
        else if (displaySpeed == DisplaySpeed.slow)
        {
            textDisplaySpeed = SLOW_DISPLAY_SPEED;
        }
    }

    public void closeDialog()
    {
        this.gameObject.SetActive(false);
        this.dialog = "";
        this.dialogTracker = 0;
    }

    /// <summary>
    /// This fuction will take in the dialog and reformat it so it fits inside the bounds of the text area
    /// </summary>
    /// <param name="dialog">The text we are trying to wrap</param>
    /// <param name="width">The width of our text box</param>
    /// <returns></returns>
    public string FitText(string dialog, float width)
    {
        string[] separatingStrings = { " " };
        string[] words = dialog.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        int currentLength = 0;
        string newText = "";
        for (int i = 0; i < words.Length; i++)
        {
            if (currentLength + words[i].Length + 1 < width)
            {
                newText += words[i] + " ";
            }
            else
            {
                newText += "\n" + words[i] + " ";
                currentLength = 0;
            }

            currentLength += words[i].Length + 1;
        }

        return newText.Trim();
    }
}

public enum DisplaySpeed{
    immediate,
    slow,
    fast
}