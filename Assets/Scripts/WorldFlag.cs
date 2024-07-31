using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFlag : MonoBehaviour
{
    public static WorldFlag Instance
    {
        get
        {
            if (_ins == null)
            {
                _ins = FindAnyObjectByType<WorldFlag>();
                if (_ins == null)
                    _ins = new GameObject(nameof(WorldFlag)).AddComponent<WorldFlag>();
            }
            return _ins;
        }
    }
    private static WorldFlag _ins;
    private Flag _flag;
    public enum Flag
    {
        DeathPlayer = 1>>0,

    }
    public void SetFlag(Flag flag)
    {
        _flag = _flag | flag;
    }
    public void RemoveFlag(Flag flag)
    {
        _flag = flag ^ _flag;
    }
    public bool IsFlag(Flag flag)
    {
        return _flag == (_flag | flag);
    }
}
