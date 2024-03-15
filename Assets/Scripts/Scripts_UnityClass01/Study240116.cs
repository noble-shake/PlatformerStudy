using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study240116 : MonoBehaviour
{
    /*
     * 2024-01-16 unity class
     * [PREVIOUS] remind for iterator.
     * [PROBLEM 1] 9 to 0 debugging using for.
     * [PROBLEM 2] 1 to 10 even and odd using for.
     * [LEARNED] double loop.
     * [PROBLEM 3] 9 x 9 dan implement.
     * [LEARNED] while loop.
     * [PROBLEM 4] 9 x 9 dan implement using while.
     */

    private void Start()
    {
        // [PROBLEM 1]
        for (int iNum = 9; iNum >= 0; iNum--) {
            Debug.Log(iNum);
        }

        // [PROBLEM 2]
        for (int iNum = 1; iNum <= 10; iNum++) { 

            if (iNum % 2 == 0 )
            {
                Debug.Log($"[{iNum}] even");
            }
            else if (iNum % 2 == 1)
            {
                Debug.Log($"[{iNum}] odd");
            }
        }

        // double loop
        for (int i1 = 9; i1 > 1; i1--) {
            for (int i2 = 1; i2 < 10; i2++) {
                Debug.Log($"{i1} X {i2} = {i1 * i2}");
            }
        }


        // while loop
        // [PROBLEM 4]
        int in1 = 9;
        int in2 = 1;
        while (in1 >= 2) {
            while (in2 <= 9) {
                Debug.Log($"{in1} X {in2} = {in1 * in2}");
                in2++;
            }
            in1--;
            in2 = 1;
        }
    }
}
