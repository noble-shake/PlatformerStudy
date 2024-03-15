using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study240112 : MonoBehaviour
{
    /*
     * 2024-01-12 unity class
     * [LEARNED] local variable and global variable description.
     * [LEARNED] previous and after value change operator c++ /  ++c ...
     * [LEARNED] if ~ else if, condtion.  >, <, >=, <=, ==, !
     * [LEARNED] and (&&), or (||)
     */

    int global_int = 10;
    bool default_check; // default bool variable check. it was false.
    // Start is called before the first frame update
    void Start()
    {
        //int iBox = 0;
        //float fBox = 0f;
        //string sBox = "null";
        //bool bBox = false;

        int a = 7;
        int b = a;
        int c = b++;
        int d = a * ++b;
        Debug.Log(d);
        a--;
        Debug.Log(a);

        def_test();
        condition_test(true);
        condition_test(false);
        condition_compare(10);
        condition_compare(11);
        condition_compare(9);
        condition_compare(0);

        condition_test(default_check);
    }

    private void def_test()
    {
        global_int++;
    }

    private void condition_test(bool condtion) {
        if (condtion)
        {
            Debug.Log($"condition is {condtion}");
        }
        else if (!condtion)
        {
            Debug.Log($"condition is {condtion}");
        }
    }

    private void condition_compare(int val) {
        if (val > 10)
        {
            Debug.Log($"condition value is {val} larger than");
        }
        else if (val < 10)
        {
            Debug.Log($"condition value is {val} lower than");
        }
        else if (val >= 10)
        {
            Debug.Log($"condition value is {val} larger or same");
        }
        else if (val <= 10)                
        {
            Debug.Log($"condition value is {val} lower or same");
        }
        else {
            Debug.Log($"condition excepted");
        }
    }

}
