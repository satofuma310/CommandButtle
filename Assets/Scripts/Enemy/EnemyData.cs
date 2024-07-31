using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = nameof(ScriptableObject) + "/" + nameof(EnemyData))]
public class EnemyData : ScriptableObject
{
    [field: SerializeField]
    public string EnemyName;
    [field: SerializeField]
    public int MaxHp;
    [field: SerializeField]
    public EnemyView EnemyPrefab;
}
