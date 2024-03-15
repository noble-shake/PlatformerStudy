using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Study240126 : MonoBehaviour
{
    /*
     * 2024-01-26 unity class
     * [PREVIOUS] Remind List Type
     * [LEARNED] List's Swallow Copy / Deep Copy.
     * [PROBLEM] Make Function with List Parameter. Conditioned by over 90. return new List.
     * [LEARNED] Lambda Expression
     * [LEANRED] Debug.Log input paramas(int, float, ...) directly logged.
     * [LEARNED] if input paramas's state is unclear, it will logged params type and address.
     * 
     * List.FindAll 입력으로는 predicate delegate function을 입력으로 받고 있어서,
     * Lambda Expression을 통해서 쉽게 값을 바꿀 수 있음.
     */

    private void Start()
    {
        testStruct cbv;
        cbv.number = 10;
        cbv.name= "struct";

        testClass cbr = new testClass();
        cbr.number = 20;
        cbr.name = "class";

        List<testStruct> listCbv = new List<testStruct>();
        listCbv.Add(cbv); //CBV 처럼 복제되어 저장
        Debug.Log($"Number = {listCbv[0].number}, Name={listCbv[0].name}");
        cbv.number = 999;
        cbv.name = "다다다";
        Debug.Log($"Number = {listCbv[0].number}, Name={listCbv[0].name}");
        cbv.number = 999;

        testStruct newCbv = listCbv[0];
        newCbv.number = 10000;
        newCbv.name = "new";
        listCbv[0] = newCbv;
        Debug.Log(newCbv);
        // listCbv[0].name = "changed?";  ~  [ERROR] List combined Struct cannot be changed. method return.

        // list swallow copy
        List<testClass> sclistCbv = new List<testClass>();

        testClass ttcbr = new testClass();
        ttcbr.number = 10;
        ttcbr.name = "ganada";

        sclistCbv.Add(ttcbr);
        sclistCbv[0].name = "ganada_plus";

        Debug.Log(ttcbr);
        Debug.Log(sclistCbv[0]);

        ttcbr = null;
        Debug.Log(ttcbr);
        Debug.Log(sclistCbv[0]);

        // list string changed? no!
        List<string> listString = new List<string>();
        string sValue = "abc";
        listString.Add(sValue);
        sValue = "abd";
        Debug.Log($"list's element is {listString[0]} and external string is {sValue} will different");

        // Different CLASS and STRUCT
        // must initialize dynamic allocation
        testClass list0 = new testClass(); 
        list0.number = 0; // if not allocated by new keyword, it will be asserted.
        testStruct struct0;
        struct0.number = 0; // already deep copied.
        
        List<testClass> list1 = new List<testClass>();
        list1.Add(list0);  // List == Call by Reference.

        // maybe Call by Reference.(Swallow Copy / Address Copy) values will changed simultaneously.
        List<testClass> list2 = list1; 
        // list alos can deep copy
        List<testClass> list3 = new List<testClass>(list1);

        // Address Check ~ ALL SAME.
        Debug.Log(list1);
        Debug.Log(list2);
        Debug.Log(list3);

        List<int> ints = new List<int>() { 90, 100, 0, 20, 80, 120 };
        List<int> result = overNumberFunction(ints);
        foreach (int i in result) { 
            Debug.Log(i);
        }
        ints = popNumberFunction(ints);

        
          
    }

    // struct => CBV
    public struct testStruct {
        public int number;
        public string name;
    }

    // class => CBR
    public class testClass{ 
        public int number;
        public string name;
    }

    List<testClass> overNumberFunction(List<testClass> iList, int socre = 90) {

        int count = iList.Count;
        List<testClass> temp = new List<testClass>();
        for (int iNum = 0; iNum < count; iNum++) {
            if (iList[iNum].number >= 90) {
                temp.Add(iList[iNum]);
            }
        }
        return temp;
    }

    List<int> overNumberFunction(List<int> iList, int socre = 90)
    {

        int count = iList.Count;
        List<int> temp = new List<int>();
        for (int iNum = 0; iNum < count; iNum++)
        {
            if (iList[iNum] >= 90)
            {
                temp.Add(iList[iNum]);
            }
        }
        return temp;
    }

    List<int> popNumberFunction(List<int> iList, int socre = 90)
    {

        List<int> temp = new List<int>(iList);
        foreach(int i in iList)
        {
            if (i <= 90) {
                temp.Remove(i);
            }
            
        }
        return temp;
    }

    List<int> lambdaFunction(List<int> iList, int score = 90) {
        //lambda expression
        // () => {};
        //iList.FindAll((x) => x >= 90);
        List<int> result = iList.FindAll((x) => x >= 90);
        return result;
    }
}