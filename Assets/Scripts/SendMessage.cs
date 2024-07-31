using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CommandSendMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _infoText;
    [SerializeField]
    private float _loadTextTime;
    private Coroutine _infoCortine;
    private static CommandSendMessage _instance;
    public static CommandSendMessage Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<CommandSendMessage>();
            return _instance;
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
    public void SendInfoMessage(string message)
    {
        if (_infoCortine != null)
            StopCoroutine(_infoCortine);
        _infoCortine = StartCoroutine(LoadMessage(message));
    }
    private IEnumerator LoadMessage(string message)
    {
        _infoText.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            var element = message.Substring(i, 1);
            _infoText.text += element;
            yield return new WaitForSeconds(_loadTextTime / message.Length);
        }
    }
}
