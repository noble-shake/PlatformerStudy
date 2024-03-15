using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Study240117 : MonoBehaviour
{
    public string star = "¡Ù";
    public string blank = "¡¡";
    /*
     * 2024-01-17 unity class
     * [PREVIOUS] remind for and while loop
     * [PROBLEM 1] make a star Pyramid using for
     * [PROBLEM 2] make a star Reverse Pyramid using for
     * [PROBLEM 3] make a star Pyramid With Blanks using for
     * [PROBLEM] make a star. using for
     * [LEARNED] double loop.
     */

    private void Start()
    {

    }

    public void problem_1_function() {
        // [PROBLEM 1] make a star Pyramid. using for
        // Condition : blank "  ", Star "¡Ù"
        Debug.Log("[PROBLEM 1] make a star Pyramid. using for");
        

        string s1Value = "";
        string total1Value = "";
        for (int iNum = 0; iNum < 5; iNum++)
        {
            s1Value += star;
            total1Value += s1Value + "\n";
        }
        Debug.Log(total1Value);

    }
    public void problem_2_function() {
        // [PROBLEM 2] make a star Reverse Pyramid with blank. using for
        Debug.Log("[PROBLEM 2] make a star Reverse Pyramid. using for");

        string total2Value = "";
        for (int iNum = 5; iNum > 0; iNum--)
        {
            string s2Value = "";
            for (int iter_num = 0; iter_num < iNum; iter_num++)
            {
                s2Value += star;
            }
            total2Value += s2Value + "\n";
        }
        Debug.Log(total2Value);
    }
    public void problem_3_function() {
        // [PROBLEM 3] make a star Pyramid with blank. using for
        Debug.Log("[PROBLEM 3] make a star Pyramid with blank. using for");

        string s3Value = "";
        string total3Value = "";
        for (int iNum = 0; iNum < 5; iNum++)
        {
            string bValue = "";
            for (int ibNum = 5 - iNum - 1; ibNum > 0; ibNum--)
            {
                bValue += blank;
            }
            s3Value += star;
            total3Value += bValue + s3Value + "\n";
        }
        Debug.Log(total3Value);
    }
    public void problem_4_function() {
        // [PROBLEM 4] make a star Reverse Pyramid with blank. using for
        Debug.Log("[PROBLEM 4] make a star Pyramid with blank. using for");

        string total4Value = "";
        for (int iNum = 5; iNum > 0; iNum--)
        {
            string s4Value = "";
            for (int iter_num = 0; iter_num < iNum; iter_num++)
            {
                s4Value += star;
            }
            string bValue = "";
            for (int ibNum = 5 - iNum; ibNum > 0; ibNum--)
            {
                bValue += blank;
            }
            total4Value += bValue + s4Value + "\n";
        }
        Debug.Log(total4Value);

    }

}
