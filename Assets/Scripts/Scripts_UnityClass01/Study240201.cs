using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Study240201 : MonoBehaviour
{
    /*
     * 2024-02-01 unity class
     * [PREVIOUS] previous input delay time -> current input delay time => ' ms '
     * [LEARNED] Update Function ~ called at each frames.
     * [LEARNED] Time Type
     * [LEARNED] Unity Engine Tools
     * [LINK] https://docs.google.com/document/d/1YcEMUSOiGZ3VWBnxACj-89cpp5gdUT2Zr7MXENDMmkU/edit?usp=sharing
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