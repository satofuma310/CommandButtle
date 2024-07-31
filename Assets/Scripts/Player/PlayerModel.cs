using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerModel : Actor, IDamage
{
    public event Action<string,int,int> OnDamage = (_,_,_) => { };
    public event Action OnDeath = () => { };
    private int _hp;
    private PlayerData _playerData;
    public PlayerModel(PlayerData playerData)
    {
        _hp = playerData.MaxHp;
        _playerData = playerData;
    }
    public void Damage(int damage)
    {
        _hp -= damage;
        OnDamage(_playerData.PlayerName, _hp, _playerData.MaxHp);
        if(_hp <= 0)
        {
            WorldFlag.Instance.SetFlag(WorldFlag.Flag.DeathPlayer);
            OnDeath();
        }
    }
}
