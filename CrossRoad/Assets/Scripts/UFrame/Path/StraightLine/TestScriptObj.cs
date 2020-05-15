/*******************************************************************
* FileName:     PathRoot.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Path.StraightLine
{

    [System.Serializable]
    public class POS : ScriptableObject
    {
        [SerializeField]
        public float x;
        [SerializeField]
        public float y;
        [SerializeField]
        public float z;
    }

    [Serializable]
    public class TestScriptObj : ScriptableObject
    {
        [SerializeField]
        public int ival;

        [SerializeField]
        public Vector3 vVal;

        [SerializeField]
        public List<POS> lvVal = new List<POS>();

        //[SerializeField]
        //List<Vector3> lvVal = new List<Vector3>();
    }

}
