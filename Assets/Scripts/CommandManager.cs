using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Command = StateMachine<CommandManager>.State;
public class CommandManager : MonoBehaviour
{
    [SerializeField]
    private TurnFacilitator _turnFacilitator;
    public TurnFacilitator TurnFacilitator => _turnFacilitator;


    private StateMachine<CommandManager> _commandStateMachine;
    public StateMachine<CommandManager> CommandStateMachine => _commandStateMachine;
    private void Awake()
    {
        _commandStateMachine = new StateMachine<CommandManager>(this);
        _commandStateMachine.OnEnterState += i =>
        {
            if (i is CommandState commandState)
            {
                SetUp(commandState);
            }
        };
        _commandStateMachine.AddTransition<EncountState, FightState>((int)TranditionType.ToFight);
        _commandStateMachine.AddTransition<EncountState, ToolState>((int)TranditionType.ToTool);
        _commandStateMachine.AddTransition<FightState, EncountState>((int)TranditionType.ToEncount);
        _commandStateMachine.AddTransition<ToolState, EncountState>((int)TranditionType.ToEncount);

        _commandStateMachine.AddAnyTransition<DoNothingState>((int)TranditionType.DoNothing);
        _commandStateMachine.AddAnyTransition<DeathState>((int)TranditionType.Death);
        _commandStateMachine.AddTransition<DoNothingState, EncountState>((int)TranditionType.ReMove);
        _commandStateMachine.Start<EncountState>();

    }
    private void SetUp(CommandState state)
    {
        SetUpUI.Instance.LineUpCommand(
            state,
            (text, command) =>
            {
                text.text = command.CommandTitleName;
            });
        CommandSendMessage.Instance.SendInfoMessage(state.CommandInfoText);
    }
    public enum TranditionType
    {
        ToEncount,
        ToFight,
        ToTool,
        Return,
        DoNothing,
        ReMove,
        Death,
    }
}
public abstract class CommandState : Command
{
    public abstract string CommandTitleName { get; }
    public abstract string CommandInfoText { get; }
    protected abstract override void OnEnter(Command prevState);
    protected abstract override void OnUpdate();
    protected abstract override void OnExit(Command nextState);
}
public class EncountState : CommandState
{
    public override string CommandTitleName => "–ß‚é";

    public override string CommandInfoText => "“G‚É‘˜‹ö‚µ‚½";

    protected override void OnEnter(Command prevState)
    {
        Debug.Log("Encount");
    }

    protected override void OnExit(Command nextState)
    {
    }

    protected override void OnUpdate()
    {
    }
}
public class FightState : CommandState
{
    public override string CommandTitleName => "í‚¤";
    public override string CommandInfoText => "‘ÎÛ‚ð‘I‘ð";


    protected override void OnEnter(Command prevState)
    {
        UIEventSetter.Instance.OnSelectEnemy += Attack;
    }

    protected override void OnExit(Command nextState)
    {
        UIEventSetter.Instance.OnSelectEnemy -= Attack;
    }
    private void Attack(EnemyView selectEnemy)
    {
        selectEnemy.OnClickImage();
        Owner.TurnFacilitator.TurnStateMachine.Dispatch((int)TurnFacilitator.TurnTranditionType.PlayerDone);
    }

    protected override void OnUpdate()
    {
    }
}
public class ToolState : CommandState
{
    public override string CommandTitleName => "“¹‹ï";
    public override string CommandInfoText => "“¹‹ï‚ð‘I‘ð";

    protected override void OnEnter(Command prevState)
    {

    }

    protected override void OnExit(Command nextState)
    {
    }

    protected override void OnUpdate()
    {
    }
}
public class DoNothingState : CommandState
{
    public override string CommandTitleName => "";
    public override string CommandInfoText => "";

    protected override void OnEnter(Command prevState)
    {
        Debug.Log("DoNothing");
    }

    protected override void OnExit(Command nextState)
    {
    }

    protected override void OnUpdate()
    {
    }
}
public class DeathState : CommandState
{
    public override string CommandTitleName => "";
    public override string CommandInfoText => "Ž€‚ñ‚Å‚µ‚Ü‚Á‚½";
    protected override void OnEnter(Command prevState)
    {
        Debug.Log("Death");
    }

    protected override void OnExit(Command nextState)
    {
    }

    protected override void OnUpdate()
    {
    }
}