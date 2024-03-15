using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Study240202 : MonoBehaviour
{
    /*
     * 2024-02-02 unity class
     * 
     */


    private void Start()
    {
        float timing = Time.deltaTime; // previous after frame - current frame error value. it designed almost 1, if 1seconds. (not same 1)
    }

    private void Update()
    {
        float speed = 10;

        // if PC display 40s per one second -> 1 / 40 = 0.025
        // if PC display 200s per one second -> 1 / 200 = 0.005
        // value = speed * time.deltatime  ->> both of PCs have same value.


    }
}