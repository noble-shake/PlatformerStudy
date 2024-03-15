using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Study240119 : MonoBehaviour
{
    /*
     * 2024-01-19 unity class
     * [PREVIOUS] remind Function.
     * [PREVIOUS] remind Rich Text.
     * [LEARNED] 
     * [PROBLEM 1] 9 x 9 dan using function. define function with input interger variable, output is Logging.
     * [PROBLEM 2] reverse 9 x 9 dan using function. define function with input interger variable, output is Logging.
     * [PROBLEM 3] 9 x 9 dan with input variables [9 2 8 3 7 4 6 5]
     */

    private void Start()
    {
        //[PROBLEM 1]
        calculateFunction(2);
        calculateFunction(5);

        //[PROBLEM 2]
        decideInputFunction(true);
        decideInputFunction(false);

        //[PROBLEM 3]
        Debug.Log("PROBLEM 3 Section");
        decideProblem3Function();
    }

    private int multiFunction(int var1, int var2) { return var1 * var2; }
    private string loggingFunction(int target, int iNum, int result) { return $"{target} X {iNum} = {result}\n"; }
    private void decideInputFunction(bool order) {
        if (order)
        {
            for (int i = 1; i < 10; i++)
            {
                calculateFunction(i);
            }
        }
        else
        {
            for (int i = 9; i > 0; i--)
            {
                calculateFunction(i);
            }
        }
    }

    private void calculateFunction(int var1) {
        string logVar = "";

        for (int iNum = 2; iNum < 10; iNum++)
        {
            int result = this.multiFunction(var1, iNum);
            logVar += this.loggingFunction(var1, iNum, result);
        }
        Debug.Log(logVar);
    }

    private void decideProblem3Function() {
        // 9 2 8 3 7 4 6 5
        // È¦¼ö / Â¦¼ö ¹øÂ°
        int count = 1;
        for (int iNum = 1; iNum < 9; iNum++)
        {

            if (iNum % 2 == 1) {
                int target = 10 - count++;
                calculateFunction(target);
            }
            else{
                int target = count;
                calculateFunction(target);
            }
        }           
    }

    private void decideProblem3Function2()
    {
        // 9 2 8 3 7 4 6 5
        // 9 - 7 = 2
        // 2 + 6 = 8
        // 8 - 5 = 3
        // -7 -> 6 -> -5 -> 4 -> -3 -> 2 -> -1 (8 iter)
    }
}