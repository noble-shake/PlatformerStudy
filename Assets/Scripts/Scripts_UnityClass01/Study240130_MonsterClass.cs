using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class MonsterExternalScript
{
    public float hp;
    public float damage;
    public float defence;
    public float level;

    public void initClassn(){
    level = 2;
}
}