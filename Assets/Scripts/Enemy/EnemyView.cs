using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class EnemyView : MonoBehaviour
{
    [field:SerializeField]
    public string ID { get; set; }
    [SerializeField]
    private TextMeshProUGUI _nameText,_hpText,_maxHpText;
    private string _name;
    private int _maxHp;
    private Image m_Image;
    public Image Image => m_Image;

    public Action OnClickImage = () => { };

    void Start()
    {
        m_Image = GetComponent<Image>();
    }
    public void UpdateHp(int hp)
    {
        _hpText.text = hp.ToString();
    }
    public void UpdateMaxHpText(int maxHp)
    {
        _maxHpText.text = maxHp.ToString();
    }
    public void UpdateNameText(string name)
    {
        _nameText.text = name;
    }

}
