using UnityEngine;
using System.Collections;

public class DialogState : State<Player>
{
    private Player player;
    public DialogBox dialogBox;

    public TextAsset textFile;

    private string[] dialog;
    private int dialogCounter;

    string[] separatingStrings = { "\n" };

    public DialogState(Player playerInstance, StateMachine<Player> fsm, TextAsset textFile)
        : base(playerInstance, fsm)
    {
        player = playerInstance;

        this.dialogBox = player.dialogBox;
        this.dialog = textFile.text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        this.dialogCounter = 0;

        Debug.Log(dialog[dialogCounter]);
        dialogBox.displayDialog(dialog[dialogCounter]);
        dialogCounter++;
    }

    override public void Enter()
    {
        player.LockControls();
    }

    override public void Execute()
    {
        Debug.Log(dialogBox.dialogField.text.Replace("\n", ""));
        if (Controls.JumpInputDown() && dialogBox.dialogField.text.Replace("\n", "") == dialog[dialogCounter - 1].Trim())
        {
            dialogBox.displayDialog(dialog[dialogCounter]);
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
        player.UnlockControls();
    }
}
