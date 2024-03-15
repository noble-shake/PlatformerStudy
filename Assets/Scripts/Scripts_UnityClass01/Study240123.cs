using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study240123 : MonoBehaviour
{
    /*
     * 2024-01-22 unity class
     * [LEARNED] Arrays
     * [LEARNED] Generic
     * [PROBLEM] input arrays function, array extending
     */

    int[] arrIntVal = new int[7]; // 7 int array memory allocate
    private void Start()
    {
        int count = arrIntVal.Length;
        Debug.Log(count);

        arrIntVal[4] = 10;

        int[] arrIntVal2 = new int[5] { 1, 2, 3, 4, 5 };
        // int[] arrIntEmptyVal = null; // reference data
        // arrIntEmptyVal[0] = 10;

        int[] extendValue = new int[3] { 1, 2, 3 };
        extendValue = new int[5]; // 1, 2, 3, 0, 0


        // [PROBLEM]
        int[] arrExam = null;
        arrExam = arrExtentionFunction(arrExam, 3);
    }

    private void intArray(int[] _value) {
        int count = arrIntVal.Length;
        for (int iNum = 0; iNum < count; iNum++) {
            _value[iNum] = 1;
        }

        //not recommend
        for (int iNum = 0; iNum < arrIntVal.Length; iNum++)
        {
            _value[iNum] = 1;
        }
    }

    private void showDebugArray<T>(T[] _value) {
        string value = "";
        int count = _value.Length;
        for (int iNum = 0; iNum < count; ++iNum) {
            value += _value[iNum];
            if (iNum != count - 1) {
                value += ", ";
            }
        }

        Debug.Log(value);
    }

    private int[] arrExtentionFunction(int[] arrVal, int extentionNum) {
        
        int[] temp = arrVal;
        int count = temp.Length;
        arrVal = new int[extentionNum];

        for (int iNum = 0; iNum < count; iNum++) {
            arrVal[iNum] = temp[iNum];
        }
        return arrVal;
    }

}