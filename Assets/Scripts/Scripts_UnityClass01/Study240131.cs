using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Study240131 : MonoBehaviour
{
    /*
     * 2024-01-31 unity class
     * [PREVIOUS] 
     * [LEARNED] Generic (Template) ~ unknown input variable or type.
     * [LEARNED] List or Dictionary implemented Generic Type.
     * [LEARNED] it seem to better than overloading, but generic can be occured as bug. (input type was class)
     * [LEARNED] SingleTon Pattern. Memory Allocated once. efficiently use.
     * [LEARNED] Data Section / Heap Section / Stack Section
     * [LEARNED] Data Section ~ Read & Write
     * [LEARNED] Heap Section ~ if need to use called, in runtime, new keyword, dynamic allocated memory, GC activate
     * [LEARNED] Stack Section ~ Function use, in compile Time, variable/paramas/returns in function,
     * if function end, return memory, performance better than heap.
     * 
     * [PROBLEM] 
     * 
     */

    private static Study240131 instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6 };
        ShowDebug(intList);
    }

    // Generic
    private void ShowDebug<T>(List<T> _value) {
        int count = _value.Count;
        string value = string.Empty;
        for (int iNum = 0; iNum < count; iNum++) {
            value += _value[iNum];
            if (iNum != count - 1) {
                value = ", ";
            }
        }
        Debug.Log(value);
    }

    // private T addFunction<T>(T _a, T _b)
    // {
        // return _a + _b;  [ERROR] Generic Type can't be operated.
        //T _c;
        //return _c;
    //}
}