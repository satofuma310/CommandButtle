using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
public class SetUpUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _commandTextPrefab;
    [SerializeField]
    private Transform _commandParent,_enemyParent;
    private Action<TextMeshProUGUI, EventTrigger, int> _onSetCommandText = (_,_,_)=> { };
    private Action<EventTrigger, EnemyView> _onSetUpEnemy = (_, _) => { };
    public event Action<TextMeshProUGUI, EventTrigger, int> OnSetCommandText
    {
        add
        {
            _onSetCommandText += value;
        }
        remove
        {
            _onSetCommandText -= value;
        }
    }
    public event Action<EventTrigger, EnemyView> OnSetUpEnemy
    {
        add
        {
            _onSetUpEnemy += value;
        }
        remove
        {
            _onSetUpEnemy -= value;
        }
    }
    private static SetUpUI _instance;
    public static SetUpUI Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<SetUpUI>();
            return _instance;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void LineUpCommand(CommandState command,Action<TextMeshProUGUI, CommandState> elementAction)
    {
        for (int i = 0; i < _commandParent.childCount; i++)
        {
            Destroy(_commandParent.GetChild(i).gameObject);
        }
        var tranditionTypeNames = Enum.GetNames(typeof(CommandManager.TranditionType));
        for (int i = 0; i < tranditionTypeNames.Length; i++)
        {
            var value = Enum.Parse<CommandManager.TranditionType>(tranditionTypeNames[i]);
            if (!command.transitions.ContainsKey((int)value)) continue;
            var commandText = Instantiate(_commandTextPrefab);
            commandText.transform.SetParent(_commandParent, false);
            elementAction(commandText, command.transitions[(int)value] as CommandState);
            _onSetCommandText(commandText, commandText.GetComponent<EventTrigger>(), (int)value);
        }
    }
    public void LineUpEnemy(EnemyData[] enemys,
        Action<EnemyData,string> lineUpDataAction = null,
        Action <EnemyView,string> lineUpViewAction = null)
    {
        for (int i = 0; i < _enemyParent.childCount; i++)
        {
            Destroy(_commandParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < enemys.Length; i++)
        {
            Guid guid = Guid.NewGuid();
            var newID = guid.ToString();


            var enemyObject = Instantiate(enemys[i].EnemyPrefab);
            enemyObject.transform.SetParent(_enemyParent, false);
            if (lineUpViewAction != null)
            {
                lineUpViewAction(enemyObject, newID);
            }
            if (lineUpDataAction != null)
                lineUpDataAction(enemys[i], newID);
            _onSetUpEnemy(enemyObject.GetComponent<EventTrigger>(), enemyObject);
        }
    }
}
