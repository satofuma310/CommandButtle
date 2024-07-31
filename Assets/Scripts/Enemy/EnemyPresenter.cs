using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class EnemyPresenter : MonoBehaviour
{
    private EnemyView[] _enemiesView = new EnemyView[0];
    private EnemyModel[] _enemiesModel = new EnemyModel[0];

    [SerializeField]
    private EnemyData[] enemiesDatas;

    [SerializeField]
    private ActorManager _actorManager;

    private void Start()
    {
        List<EnemyModel> enemyModelList = new List<EnemyModel>();
        List<EnemyView> enemyViewList = new List<EnemyView>();
        SetUpUI.Instance.LineUpEnemy(enemiesDatas,
        (data,id) => 
        {
            var newModel = new EnemyModel(data);
            newModel.ID = id;
            enemyModelList.Add(newModel);
            _actorManager.AddActor(newModel,1);
        }, 
        (view,id) =>
        {
            enemyViewList.Add(view);
            view.ID = id;
        });
        _enemiesModel = enemyModelList.ToArray();
        _enemiesView = enemyViewList.ToArray();




        for (int i = 0; i < _enemiesModel.Length; i++)
        {
            var viewIndex = GetViewIndex(_enemiesModel[i].ID);
            var index = i;
            if (viewIndex == -1) 
            {
                print($"インデックスを取得できませんでした id:{_enemiesModel[i].ID}");
                continue; 
            }
            _enemiesView[viewIndex].OnClickImage += () => _enemiesModel[index].Damage(1);
            _enemiesModel[i].OnDamage +=
                (name, hp) =>
                {
                    _enemiesView[viewIndex].UpdateNameText(name);
                    _enemiesView[viewIndex].UpdateHp(hp);
                };
        }
    }
    private int GetViewIndex(string id)
    {
        var index = _enemiesView.ToList().IndexOf(x=>x.ID == id);
        return index;
    }
}
public static class ListEx
{
    public static int IndexOf<T>(this List<T> list, Func<T,bool> func)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (func(list[i]))
            {
                return i;
            }
        }
        return -1;
    }
}