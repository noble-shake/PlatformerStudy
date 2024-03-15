using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Study240122 : MonoBehaviour
{
    /*
     * 2024-01-22 unity class
     * [PREVIOUS] remind Function with PROBLEMs.
     * [LEARNED] Various Parameters (Qualifiers) in Function (out, ref, in)
     * [LEARNED] out - not care about return type. have to initialize in function.
     * [LEARNED] tuple
     * [LEARNED] var
     * [LEARNED] call by value / call by reference
     * [LEARNED] ref ~ func type func_name (ref variable) -> even variable type is CBV, make CBR.
     * [PROBLEM] swap
     * [LEARNED] in ~ use for performance, address as parameters but, never value changed. totally same with ref (READ ONLY)
     * 
     * [memory allocation]
     * C - malloc / free
     * C++ - new / delete
     * C# - new / not exist (Garbage Collector)
     * Unity - Destory() ~ if object no rendered. using GC
     * Unity - Resources.Unload ~~ functions -> call GC. if scene change or screen fade out.
     * ** 찾아보니 C# 도 마찬가지로 Reference Count 영향을 받아서 동작하는 것 같음.
     */

    private void Start()
    {
        int value1 = addFunction(1, 2);
        addFunction2(1, 2, out int value2);
        Debug.Log(value1);
        Debug.Log(value2);
        bool check_done = addFunction3(1, 2, out int value3add, out int value3minus, out int value3Multi, out int value3Div);
        (int _plus, int _minus, int _multi, int _div) value = someFunction(1, 2);
        int p = value._plus;
        Debug.Log($"{value._multi}");

        var varValue1 = 0; // defined in  compiler process  
        var varValue2 = someFunction(1, 2);

        // blue -> call by value (CBV -> for keeping value invariant), more than more copy value, memory consumption increased.
        int vvvvv1 = 0;
        int vvvvv2 = vvvvv1;

        // green -> call by reference (CBR), refer allocated memory points (Address) by [RAM] ~ if these value changed, original data also changed. 
        // perforamnce : CBV < CBR
        values valClass1 = new values(); // address
        values valClass2 = valClass1; // these case two classes will be same.

        // CBR example
        int cbr_value = 1;
        Debug.Log($"CBR PREVIOUS {cbr_value}");
        bool cbr_check = someFunction2(ref cbr_value);
        Debug.Log($"CBR AFTER {cbr_value}");

        // [PROBLEM] swap
        int iValue1 = 10;
        int iValue2 = 20;

        Debug.Log($"{iValue1} <-> {iValue2}");
        swap(ref iValue1, ref iValue2);
        Debug.Log($"{iValue1} <-> {iValue2}");
    }

    private void swap(ref int _a, ref int _b) {
        // a <= b's value
        // b <= a's value
        int temp = _b;
        _b = _a;
        _a = temp;
    }

    public class values { }
    private int addFunction(int _a, int _b) {
        return _a + _b;
    }
    
    //if we want use 'not only int'.. 'out' qualifier will help
    private void addFunction2(int _a, int _b, out int _result) {
        _result = default;  // don't need return keyword
    }

    private bool addFunction3(int _a, int _b, out int _resultAdd, out int _resultMinus, out int _resultMulti, out int _resultDiv)
    {
        _resultAdd = default;
        _resultMinus = default;  
        _resultMulti = default;  
        _resultDiv = default;

        return true;
    }

    private (int _plus, int _minus, int _multi, int _div) someFunction(int _a, int _b) {
        return (_a + _b, _a - _b, _a * _b, _a / _b);
    }

    private bool someFunction2(ref int value) {
        value += 1;
        return true;
    }

}