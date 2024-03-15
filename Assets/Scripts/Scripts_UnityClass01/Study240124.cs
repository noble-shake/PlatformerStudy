using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Study240124 : MonoBehaviour
{
    /*
     * 2024-01-24 unity class
     * [PREVIOUS] Remind Arrays
     * [PROBLEM 1] Given Function with Parameters grades (0 ~ 100), return 90 scored value.
     * [PROBLEM 2] Given Function with Parameters Arrays and bools, if true, bottom-up, if false, top-down... I Solved using Insertion Sort.
     * [LEARNED] 2-dimensional arrays.
     * [PROBLEM 3] make 2-dim arrays and logging Function as one-line. implements Function with 2dim arrays and condition Level(1, 2).
     * [PROBLEM 3] Level1 is normal dimension order log, level2 is reverse dimenstion order log. 
     */

    private void Start()
    {
        int[] problemVal = new int[5] { 50, 75, 90, 95, 100};
        int[] problemVal2 = new int[6] { 1, 9, 8, 99, 6, 5};
        int[] problemVal3 = new int[6] { 1, 9, 8, 99, 6, 5};
        int[] result1 = overScoredFunction(problemVal);
        
        Debug.Log(result1);
        int result_count = result1.Length;
        for (int iNum = 0; iNum < result_count; iNum++) {
            Debug.Log(result1[iNum]);
        }

        // [PROBLEM 1]
        int[] result2 = sortFunction(problemVal2);
        result_count = result2.Length;
        for (int iNum = 0; iNum < result_count; iNum++)
        {
            Debug.Log(result2[iNum]);
        }

        // [PROBLEM 2]
        int[] result3 = sortFunction(problemVal3, true);
        result_count = result3.Length;
        for (int iNum = 0; iNum < result_count; iNum++)
        {
            Debug.Log(result3[iNum]);
        }

        // 2-dimensional
        int[,] arr2dim3 = new int[3, 3];

        // not recomended
        int[,] arr2dim = new int[,] { { 1, 2 }, { 2, 4 } };
        int[,] arr2dim2 = new int[2,3] { { 1, 2, 3}, { 2, 4, 6 } };
        
        // int count = arr2dim2.Length;
        int dim1Count = arr2dim2.GetLength(0);
        for (int iNum = 0; iNum < dim1Count; iNum++) {
            Debug.Log(arr2dim2[iNum, 0]);
        }

        // [PROBLEM 3]
        int[,] problemArr = new int[3, 3];
        LoggingFunction(problemArr);
        LoggingFunction(problemArr, 2);
    }

    private int[] overScoredFunction(int[] target, int score=90) {

        int count = target.Length;
        int[] copy_target = new int[count];
        int temp_count = 0;
        for (int iNum=0; iNum < count; iNum++) {
            if (target[iNum] >= score) {
                copy_target[temp_count++] = target[iNum];
            }
            
        }
        int[] temp = new int[temp_count];
        count = temp.Length;
        for (int iNum=0; iNum < count; iNum++)
        {
            temp[iNum] = copy_target[iNum];
        }
        return temp;
    }

    private int[] sortFunction(int[] target, bool topDownCheck=false)
    {

        int count = target.Length;
        int[] copy_target = new int[count];

        for (int jNum = 0; jNum < count; jNum++) {
            int max_idx = jNum;
            for (int iNum = jNum; iNum < count; iNum++)
            {
                if (topDownCheck == true)
                {
                    if (target[max_idx] < target[iNum])
                    {
                        max_idx = iNum;
                    }
                    else
                    {
                        continue;
                    }
                }
                else {
                    if (target[max_idx] > target[iNum])
                    {
                        max_idx = iNum;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            copy_target[jNum] = target[max_idx];
            target[max_idx] = target[jNum];
        }

        return copy_target;
    }

    private void LoggingFunction(int[,] arrVal, int Level = 1) { 
        int lenX = arrVal.GetLength(0);
        int lenY = arrVal.GetLength(1);
        Debug.Log($"Level {Level}");

        string oneLine = "";
        if (Level == 1) {
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    oneLine += $"({j},{i})";
                    if (j != lenY -1) {
                        oneLine += ",";
                    }
                }
                oneLine += "\n";
            }
        }
        else if (Level == 2)
        {
            for (int i = lenX-1; i > -1; i--)
            {
                for (int j = lenY-1; j > -1; j--)
                {
                    oneLine += $"({i},{j})";
                    if (j != 0)
                    {
                        oneLine += ",";
                    }
                }
                oneLine += "\n";
            }
        }

        Debug.Log(oneLine);

    }
}