using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
public class EnemyModel :Actor, IDamage
{
    public EnemyModel(EnemyData enemyData)
    {
        Data = enemyData;
        Name = enemyData.EnemyName;
        HP = enemyData.MaxHp;
    }
    public string Name { get; private set; }
    public int HP { get; private set; }
    public int Power { get; private set; }
    public string ID { get; set; }
    public EnemyData Data { get; private set; }
    public event Action<string,int> OnDamage = (_,_) => { };

    public void Damage(int damage)
    {
        HP -= damage;
        OnDamage(Name,HP);
    }
    public async Task RequestAttack()
    {
        CommandSendMessage.Instance.SendInfoMessage($"{Name}‚ÌUŒ‚");
        await Task.Delay(1000);
        var maskTeam = Manager.GetMaskTeams(1);
        var randomIndex = Random.Range(0, maskTeam.Length);
        var randomActor = maskTeam[randomIndex]
            .Actors[Random.Range(0, maskTeam[randomIndex].Actors.Length)];

        if(randomActor is IDamage damage)
        {
            damage.Damage(1);
        }
        CommandSendMessage.Instance.SendInfoMessage($"{1}‚Ìƒ_ƒ[ƒW‚ğó‚¯‚½");
        await Task.Delay(1000);

    }
}
public interface IDamage
{
    public void Damage(int damage);
}