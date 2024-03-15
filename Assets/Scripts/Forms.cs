using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTag { 
    None,
    Enemy,
    Player,
    Item,
}

// static -> memory allocated, when Game Started.
public static class Tool {
    public static string GetGameTag(GameTag _value) {
        return _value.ToString();
    }

    public static bool IsEnterFirstScene = false;
}

// just research.
[System.Serializable]
public class Tool2 {
    public string GetGameTag(GameTag _value)
    {
        return _value.ToString();
    }
}
