using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnState = StateMachine<TurnFacilitator>.State;
public class TurnFacilitator : MonoBehaviour
{
    [SerializeField]
    private ActorManager _actorManager;
    public ActorManager ActorManager => _actorManager;

    [SerializeField]
    private CommandManager _commandManager;
    public CommandManager CommandManager => _commandManager;
    private StateMachine<TurnFacilitator> _turnStateMachine;
    public StateMachine<TurnFacilitator> TurnStateMachine => _turnStateMachine;

    void Start()
    {
        _turnStateMachine = new StateMachine<TurnFacilitator>(this);
        _turnStateMachine.AddTransition<PlayerTurn, EnemyTurn>((int)TurnTranditionType.PlayerDone);
        _turnStateMachine.AddTransition<EnemyTurn, PlayerTurn>((int)TurnTranditionType.EnemiesDone);
        _turnStateMachine.Start<PlayerTurn>();
    }
    public enum TurnTranditionType
    {
        PlayerDone,
        EnemiesDone,
    }

    void Update()
    {
        
    }
}
public class PlayerTurn: TurnState
{
    protected override void OnEnter(TurnState prevState)
    {
        if(Owner.CommandManager.CommandStateMachine.CurrentState.stateMachine.CurrentState is DoNothingState)
        Owner.CommandManager.CommandStateMachine.CurrentState.stateMachine.Dispatch((int)CommandManager.TranditionType.ReMove);
    }
    protected override void OnExit(TurnState nextState)
    {
    }
}
public class EnemyTurn: TurnState
{
    protected async override void OnEnter(TurnState prevState)
    {
        Owner.CommandManager.CommandStateMachine.CurrentState.stateMachine.Dispatch((int)CommandManager.TranditionType.DoNothing);
        Debug.Log("Enter");
        var team = Owner.ActorManager.GetTeam(1);
        foreach (var actor in team.Actors)
        {
            if(actor is EnemyModel enemy)
            {
                await enemy.RequestAttack();
            }
            if (WorldFlag.Instance.IsFlag(WorldFlag.Flag.DeathPlayer))
            {
                Owner.CommandManager.CommandStateMachine.CurrentState.stateMachine.Dispatch((int)CommandManager.TranditionType.Death);
                break;
            }
        }
        stateMachine.Dispatch((int)TurnFacilitator.TurnTranditionType.EnemiesDone);
    }
}