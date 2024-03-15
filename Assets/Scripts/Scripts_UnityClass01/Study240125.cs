using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Study240125 : MonoBehaviour
{
    /*
     * 2024-01-25 unity class
     * [PREVIOUS] Remind Arrays and multi-dimensional Arrays
     * [PREVIOUS] Remind Params. (ref, out, in, params)
     * [LEARNED] string different with other type. (implemented as class, other types implemented as 'struct')
     * [LEARNED] struct -> CBV / class -> CBR
     * [LEARNED] string is CBR but, this type excepted as CBV, moreover string was class.
     * [LEARNED] string casting
     * [LEARNED] Regex.Replace (RegularExpressions.Replace), string.Split(), string,Substring()
     * [LEARNED] Swallow Copy ~ likely to CBR, only copied address / Deep Copy ~ ikely to CBV, newaly allocated address and copy value
     * [LEARNED] List Type
     * 
     * CBV : memory copy and allocated new address.
     * CBR : allocated address copy.
     * normally CBR performance better than CBV, but in struct, both performance will flexible.
     */

    private void Start()
    {

        // String Casting
        // example 1
        string dummyValue = "EnglishInsertedInteger 0123";
        
        // Regex.Replace(target string, pattern, replaced), various overloading functions exists (Regex.Replace("text", "teex"))
        string temp1 = Regex.Replace(dummyValue, @"\D", ""); // not integer texts replace to blank.
        int dummyInteger1 = int.Parse(temp1);
        Debug.Log(dummyInteger1);
        //string temp2 = regex.replace(dummyvalue, @"\d", "");
        //dummyinteger1 = int.parse(temp2);
        //debug.log(dummyinteger1);

        // example 2
        string dummyValue2 = "0123";
        int dummyInteger2 = int.Parse(dummyValue2);
        Debug.Log(dummyInteger2);

        // example 3
        string dummyValue3 = "texxxt";
        dummyValue3 = dummyValue3.Replace("te", "fe");
        Debug.Log(dummyValue3);

        string fruits = "apple,orange,lemon";
        string[] fruit = fruits.Split(",");
        showDebugArray(fruit);

        // Swallow Copy
        int[] arrInt1 = new int[3] { 0, 1, 2 };
        showDebugArray(arrInt1);
        int[] arrInt2 = arrInt1;
        arrInt2[0] = 3; // arrInt1[0] also changed.
        showDebugArray(arrInt1);
        showDebugArray(arrInt2);

        // Deep Copy, Likely to CBV, Unity not officially used.
        int[] arrInt3 = copyArray(arrInt1);
        arrInt3[1] = 5;
        showDebugArray(arrInt1);
        showDebugArray(arrInt3);

        // List Type - Add / Remove / Clear / Count etc...
        List<int> listVal = new List<int>();
        listVal.Add(3);
        listVal.Add(5);
        listVal.Add(6);
        listVal.Add(8);

        Debug.Log("Original List");
        string logging = "";
        foreach (int val in listVal) {
            logging += val + ",";
        }
        Debug.Log(logging);

        logging = "";
        Debug.Log("Remove 5");
        listVal.Remove(5);
        foreach (int val in listVal)
        {
            logging += val + ",";
        }
        Debug.Log(logging);

        logging = "";
        Debug.Log("RemoveAt 0");
        listVal.RemoveAt(0);
        foreach (int val in listVal)
        {
            logging += val + ",";
        }
        Debug.Log(logging);

    }

    private int[] copyArray(int[] _value)
    {
        int[] copy = new int[_value.Length];
        _value.CopyTo(copy, 0); // if 0, Copied [0 ~ Length]
        return copy;
    }

    // for Logging.
    private void showDebugArray<T>(T[] _value)
    {
        string value = "";
        int count = _value.Length;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            value += _value[iNum];
            if (iNum != count - 1)
            {
                value += ", ";
            }
        }

        Debug.Log(value);
    }
}