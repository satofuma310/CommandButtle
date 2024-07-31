using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private PlayerData _playerData;
    [SerializeField]
    private ActorManager _actorManager;
    private PlayerModel _playerModel;
    private void Start()
    {
        _playerModel = new PlayerModel(_playerData);
        _actorManager.AddActor(_playerModel,0);
        _playerView.SetUp(_playerData);
        _playerModel.OnDamage += (name, hp, maxHp) =>
        {
            _playerView.UpdateName(name);
            _playerView.UpdateHp(hp);
        };
        _playerModel.OnDeath += () =>
        {
            CommandSendMessage.Instance.SendInfoMessage("");
        };
    }
}
