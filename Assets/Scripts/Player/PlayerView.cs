using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerNameText;
    [SerializeField]
    private Transform _heartParent;
    [SerializeField]
    private Image _heartPrefab;
    public void UpdateName(string name)
    {
        _playerNameText.text = name;
    }
    public void UpdateHp(int hp)
    {
        for (int i = 0; i < _heartParent.childCount; i++)
        {
            Destroy(_heartParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < hp; i++)
        {
            var heart = Instantiate(_heartPrefab);
            heart.transform.SetParent(_heartParent, false);
        }
    }
    public void SetUp(PlayerData data)
    {
        UpdateName(data.PlayerName);
        UpdateHp(data.MaxHp);

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
