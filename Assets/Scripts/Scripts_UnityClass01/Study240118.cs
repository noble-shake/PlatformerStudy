using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Study240118 : MonoBehaviour
{
    /*
     * 2024-01-18 unity class
     * [PREVIOUS] remind [make star pyramid] using "for". then use "while"
     * [LEARNED] Function.
     * [LEARNED] Unity Life Cycle.  https://docs.unity3d.com/Manual/ExecutionOrder.html
     * [LEARNED] Awake - Start - FixedUpdate - Update - LateUpdate 
     * [LEARNED] return - if only return or zero(0) return, exit function. or not, return pre-defining variable.
     * [LEARNED] overloading ~ same function name, classify different input / output cases
     * [PROBLEM 1] Plus Function 
     * [PROBLEM 2] Minus Function 
     * [PROBLEM 3] Multiply Function 
     * [PROBLEM 4] Divide Function 
     * [LEARNED] rich text for visibility https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html
     */

    // import other script
    public Study240117 previous_study;

    private void Awake()
    {
        // First Called. Only once run.
        // normally used define itself
        // all of Awake function order is random. setting from Project Setting "Script Execution Order"
        Debug.Log("Awake");
        
    }

    private void Start()
    {
        // Second Called. Only once run.
        // get reference from other scripts.
        Debug.Log("Start");
        previous_study = GetComponent<Study240117>();
        previous_study.problem_1_function();
        previous_study.problem_2_function();
        previous_study.problem_3_function();
        previous_study.problem_4_function();

        ExamFunction();
        ExamFunction(1);
        ExamFunction(0.1f);
        ExamFunction("null");
        ExamFunction(1, "null", "haha", "heh heh");
        Debug.Log(ExamFunction(1, 2, 3));
        
        //[PROBLEM 1] Plus Function
        //[PROBLEM 2] Minus Function 
        //[PROBLEM 3] Multiply Function 
        //[PROBLEM 4] Divide Function 
        PlusFunction(3, 1);
        MinusFunction(3, 1);
        MultiplyFunction(3, 3);
        DivideFunction(3, 2);

        Debug.Log($"<color=red> text example. </color>");
    }

    private void FixedUpdate()
    {
        // not related frames. has specific timing.(0.02s) recommended use Physics implement
        Debug.Log("FixedUpdate");
    }
    private void Update()
    {
        // each framed run. algoritm works
        Debug.Log("Update");
    }

    private void LateUpdate()
    {
        // same with update but, need to check at alst. Camera Moving
        Debug.Log("LateUpdate");
    }

    private void ExamFunction() { }
    private int ExamFunction(int var) { Debug.Log(var); return var; }
    private float ExamFunction(float var) { Debug.Log(var); return var; }
    private string ExamFunction(string var) { Debug.Log(var); return var; }
    private string ExamFunction(params int[] var) // undefined parameters
    { 
        int count = var.Length;
        string data = "";
        foreach (int v in var)
        {data += $"Function Exam {v}\n";}
        return data; 
    }

    private void ExamFunction(int var1, params string[] var2) // undefined parameters
    {
        int count = var2.Length;
        Debug.Log($"Function Exam {var1}");
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"Function Exam {var2[i]}");
        }
        return;
    }

    private int PlusFunction(int var1, int var2) {
        return var1 + var2;
    }
    private int MinusFunction(int var1, int var2)
    {
        return var1 - var2;
    }
    private int MultiplyFunction(int var1, int var2)
    {
        return var1 * var2;
    }
    private int DivideFunction(int var1, int var2)
    {
        return var1 / var2;
    }
}


