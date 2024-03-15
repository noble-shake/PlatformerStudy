using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Study240129 : MonoBehaviour
{
    /*
     * 2024-01-29 unity class
     * [PREVIOUS] List Type ~ list automatically push or pull, if data inserted or deleted.
     * [LEARNED] Dictionary Type - performance better than List
     * [LEARNED] Arrays or List address allocated 0 to ~ .., Dictionary can't access normal index count
     * [LEARNED] Can't use key repeatly in Dict
     * [LEARNED] Build Setting -> Development Build Check ~ Debugging mode.
     * [LEARNED] Class
     * [LEARNED] SerializeField, System.Serializable 
     * [PROBLEM] 
     * 
     * Inspector - Unity Engine / Designer / PM workspace
     * Priority higher than Script
     */
    [SerializeField] public Monster monClass; 
    [SerializeField] private List<int> sList; // dynamically allocated in inspector
    public List<int> publicList;

    private void Start()
    {
        //[PREVIOUS] List Type ~ list automatically push or pull, if data inserted or deleted.
        List<int> intList = new List<int>();
        intList.Add(0);
        intList.Add(1);
        intList.Add(2);

        int count = intList.Count;
        for (int iNum = count - 1; iNum > -1; iNum--) {
            intList.Remove(iNum);
        }
        Debug.Log(intList.Count);
        //int count = intList.Count;
        //for (int iNum = 0; iNum < count; iNum++) {
        //    intList.Remove(iNum); // [0, 1, 2],  0 deleted, 2 deleted.. index ERROR
        //}


        // [LEARNED] Dictionary Type - performance better than List
        // normally used for static data management.
        Dictionary<int, string> exampleDict = new Dictionary<int, string>();
        for (int iNum = 0; iNum < 5; iNum++) {
            exampleDict[iNum] = $"{iNum}";
            exampleDict.Add(5 + iNum, $"{iNum}");
            Debug.Log(exampleDict[iNum]);
        }

        count = exampleDict.Count;
        for (int iNum = count - 1; iNum > -1; iNum--)
        {
            Debug.Log(exampleDict[iNum]);
        }

        Dictionary<(float, float), string> mapDict = new Dictionary<(float, float), string>();
        Dictionary<string, string> stringDict = new Dictionary<string, string>();
        stringDict["null"] = "nullException";
        Debug.Log(stringDict["null"]);

        if (stringDict.ContainsKey("null")) {
            Debug.Log("key exist");
        } // key exist check.

        List<string> keyList = stringDict.Keys.ToList();
        string[] strArr = stringDict.Values.ToArray();
        List<string> strList = stringDict.Values.ToList();

        // stringDict.Try~  -> out params ÀÌ¿כ, if exist, return value.
        if (stringDict.TryGetValue("null", out string value)) {
            Debug.Log(value);
        }

        // call by reference as key?
        refClass examInstance = new refClass();
        Dictionary<refClass, string> refDict = new Dictionary<refClass, string>();
        refDict.Add(new refClass(), "example Instance 1");
        refDict.Add(examInstance, "example Instance 2");
        List<refClass> refkeyList = refDict.Keys.ToList();
        Debug.Log(refkeyList[0]);
        Debug.Log(refkeyList[1]); // same output
        Debug.Log(refDict[refkeyList[0]]);
        Debug.Log(refDict[refkeyList[1]]); // different output. address return, but user can't see.
        if (refkeyList[0] == refkeyList[1]) { Debug.Log("same address"); } // condition false.

        examInstance = null;
        refkeyList = refDict.Keys.ToList();
        Debug.Log(refkeyList[0]);
        Debug.Log(refkeyList[1]); // same output
        Debug.Log(refDict[refkeyList[0]]);
        Debug.Log(refDict[refkeyList[1]]); // different output. address return, but user can't see.
        // refDict[refkeyList[0]].check; [ERROR] can't use as class instance.



    }
    public class refClass
    {
        bool check = false;
    }


    // serialization will be edited in inspector bar.
    [System.Serializable]
    public class Monster {
        public float hp;
        public float damage;
        public float defence;
        public float level;
    } 
}