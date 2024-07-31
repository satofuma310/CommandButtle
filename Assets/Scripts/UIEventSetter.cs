using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class UIEventSetter : MonoBehaviour
{
    [SerializeField]
    private Color
        _commandSelectColor,
        _commandDefaultColor,
        _enemySelectColor,
        _enemyDefaultColor;
    [SerializeField]
    private CommandManager _commandManager;
    private Action<CommandState> _onSelectCommand = _=> { };
    private Action< EnemyView> _onSelectEnemy = _=> { };
    public event Action<CommandState> OnSelectCommand
    {
        add
        {
            _onSelectCommand += value;
        }
        remove
        {
            _onSelectCommand -= value;
        }
    }
    public event Action< EnemyView> OnSelectEnemy
    {
        add
        {
            _onSelectEnemy += value;
        }
        remove
        {
            _onSelectEnemy -= value;
        }
    }
    public static UIEventSetter Instance
    {
        get
        {
            if (_ins == null)
                _ins = FindAnyObjectByType<UIEventSetter>();
            return _ins;
        }
    }
    private static UIEventSetter _ins;
    void Awake()
    {
        SetUpUI.Instance.OnSetCommandText += (text, trigger,id) =>
        {
            text.color = _commandDefaultColor;
            trigger.AddPointerEnterListener(_ => text.color = _commandSelectColor);
            trigger.AddPointerExitListener(_ => text.color = _commandDefaultColor);
            trigger.AddPointerUpListener(_ => _commandManager.CommandStateMachine.CurrentState.stateMachine.Dispatch(id));
        };
        SetUpUI.Instance.OnSetUpEnemy += (trigger, controller) =>
        {
            trigger.AddPointerEnterListener(_ => controller.Image.color = _enemySelectColor);
            trigger.AddPointerExitListener(_ => controller.Image.color = _enemyDefaultColor);
            trigger.AddPointerUpListener(_ => _onSelectEnemy(controller));
        };
    }

    void Update()
    {

    }
}
