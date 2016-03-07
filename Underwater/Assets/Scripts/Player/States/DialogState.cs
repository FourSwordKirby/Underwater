using UnityEngine;
using System.Collections;

public class DialogState : State<Player>
{
    private Player player;
    //public DialogBox dialogBox;

    public TextAsset textFile;

    private string[] dialog;
    private int dialogCounter;

    string[] separatingStrings = { "\n" };

    public DialogState(Player playerInstance, StateMachine<Player> fsm, TextAsset textFile)
        : base(playerInstance, fsm)
    {
        player = playerInstance;

        this.dialog = textFile.text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        this.dialogCounter = 0;

        Debug.Log(dialog[dialogCounter]);
        //dialogBox.displayDialog(name, dialog[dialogCounter]);
        dialogCounter++;
    }

    override public void Enter()
    {
    }

    override public void Execute()
    {
        if (Controls.JumpInputDown()) //&& dialogBox.dialogField.text == dialog[dialogCounter-1])
        {
            Debug.Log(dialog[dialogCounter]);
            dialogCounter++;
        }

        if (dialogCounter == dialog.Length)
        {
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
        }
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
    }
}
