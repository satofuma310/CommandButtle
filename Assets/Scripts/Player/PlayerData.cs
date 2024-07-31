using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =nameof(ScriptableObject) + "/" + nameof(PlayerData))]
public class PlayerData : ScriptableObject
{
    [field: SerializeField]
    public string PlayerName;
    [field: SerializeField]
    public int MaxHp;
}
