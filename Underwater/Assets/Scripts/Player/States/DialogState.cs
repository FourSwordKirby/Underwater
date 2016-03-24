using UnityEngine;
using System.Collections;

public class DialogState : State<Player>
{
    private Player player;
    public DialogBox dialogBox;

    public TextAsset textFile;

    private string[] dialog;
    private int dialogCounter;

    private Player.PlayerControls desiredControl;

    string[] separatingStrings = { "\n" };

    public DialogState(Player playerInstance, StateMachine<Player> fsm, TextAsset textFile, Player.PlayerControls desiredControl = Player.PlayerControls.None)
        : base(playerInstance, fsm)
    {
        player = playerInstance;

        this.desiredControl = desiredControl;

        this.dialogBox = player.dialogBox;
        this.dialog = textFile.text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        this.dialogCounter = 0;

        dialogBox.displayDialog(dialog[dialogCounter]);
        dialogCounter++;
    }

    override public void Enter()
    {
        player.anim.SetFloat("MoveSpeed", 0.0f);
        player.anim.SetBool("Airborne", false);

        dialogBox.gameObject.SetActive(true);
        player.LockControls();
    }

    override public void Execute()
    {
        if (Controls.JumpInputDown() && dialogCounter == dialog.Length
            && dialogBox.dialogField.text.Replace("\n", "") == dialog[dialogCounter - 1].Trim())
        {
            dialogBox.gameObject.SetActive(false);
            player.ActionFsm.ChangeState(new IdleState(player, player.ActionFsm));
            return;
        }

        if (Controls.JumpInputDown())
        {
            if (dialogBox.dialogField.text.Replace("\n", "") == dialog[dialogCounter - 1].Trim())
            {
                dialogBox.displayDialog(dialog[dialogCounter]);
                dialogCounter++;
            }
            else
            {
                dialogBox.forceDialog(dialog[dialogCounter - 1].Trim());
            }
        }
    }

    override public void FixedExecute()
    {
    }

    override public void Exit()
    {
        player.UnlockControls();
        dialogBox.gameObject.SetActive(false);
        //Show the instruction if it is set
        if (desiredControl != Player.PlayerControls.None)
        {
            player.showControl(desiredControl);
        }
    }
}
