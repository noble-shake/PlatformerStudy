using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Study240115 : MonoBehaviour
{
    /*
     * 2024-01-15 unity class
     * [PREVIOUS] remind variable / global variable & local variable
     * [PREVIOUS] remind if ~ else 
     * [PREVIOUS] remind debugging 
     * [LEARNED] simply public / private 
     * [LEARNED] += / -= / *= operator.
     * [LEARNED] swich expression.
     * [PROBLEM] Manage Grade using Switch and if ~ else
     * [LEARNED] for iteration.
     */

    public int iNum = 0;
    // private int iGold= 0;
    private void Start()
    {
        int iNum = 187;
        Debug.Log(iNum % 100);
        Debug.Log(iNum % 10);
        Debug.Log(iNum.ToString()[0]);
        Debug.Log(iNum.ToString()[iNum.ToString().Length / 2]);

        // Problem : Managing Grade
        /*
         * [Coindition] int grade
         * [Coindition] 100 == grade, then A
         * [Coindition] 95 >= grade, then B
         * [Coindition] 90 >= grade, then C
         * [Coindition] 85 >= grade then D
         * [Coindition] 80 >= grade then E
         * [Coindition] except -> F
         */
        int grade = 100;
        grade_manage(grade);
        grade = 80;
        grade_manage(grade);
        grade = 79;
        grade_manage(grade);
        grade = 98;
        grade_manage(grade);
        grade = 86;
        grade_manage(grade);
        grade = 4;
        grade_manage(grade);

        grade = 100;
        grade_manage_switch(grade);
        grade = 80;
        grade_manage_switch(grade);
        grade = 79;
        grade_manage_switch(grade);
        grade = 98;
        grade_manage_switch(grade);
        grade = 86;
        grade_manage_switch(grade);
        grade = 4;
        grade_manage_switch(grade);

        int max_val = 10;
        for (int i = 0; i < max_val; i++) {
            Debug.Log($"[{i}/{max_val}]");
        }
    }

    public void grade_manage(int grade) {
        string rank = "F";
        if (grade == 100)
        {
            rank = "A";
        }
        else if (grade >= 95)
        {
            rank = "B";
        }
        else if (grade >= 90)
        {
            rank = "C";
        }
        else if (grade >= 85)
        {
            rank = "D";
        }
        else if (grade >= 80)
        {
            rank = "E";
        }
        else
        {
            rank = "F";
        }
        Debug.Log($"grade = {grade}, Rank = {rank}");
    }

    public void grade_manage_switch(int grade) {
        string rank = "F";
        switch (grade) {
            case 100:
                rank = "A";
                break;
            case >=95:
                rank = "B";
                break;
            case >=90:
                rank = "C";
                break;
            case >=85:
                rank = "D";
                break;
            case >=80:
                rank = "E";
                break;
            default:
                rank = "F";
                break;
        }
        Debug.Log($"grade = {grade}, Rank = {rank}");
    }
}
