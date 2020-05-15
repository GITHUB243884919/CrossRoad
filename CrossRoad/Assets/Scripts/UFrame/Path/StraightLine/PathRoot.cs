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
    [Serializable]
    public class ScriptableObjectDictionary<TKey, TValue> : ScriptableObject
    {
        [SerializeField]
        public List<TKey> keys = new List<TKey>();
        [SerializeField]
        public List<TValue> values = new List<TValue>();
        protected Dictionary<TKey, TValue> target = new Dictionary<TKey, TValue>();
        public Dictionary<TKey, TValue> Target
        {
            get
            {
                return target;
            }
            set
            {
                ////target = value;
                //keys = new List<TKey>(value.Keys);
                //values = new List<TValue>(value.Values);

                //for(int i = 0; i < keys.Count; i++)
                //{
                //    target.Add(keys[i], values[i]);
                //}

                target = value;
                keys = new List<TKey>(Target.Keys);
                values = new List<TValue>(Target.Values);
            }
        }

        public void OnBeforeSerialize()
        {
            keys = new List<TKey>(Target.Keys);
            values = new List<TValue>(Target.Values);
        }

        public void OnAfterDeserialize()
        {
            int count = Math.Min(keys.Count, values.Count);
            target = new Dictionary<TKey, TValue>(count);
            for (int i = 0; i < count; ++i)
            {
                try
                {
                    Target.Add(keys[i], values[i]);
                }
                catch (Exception e)
                {
                    Logger.LogWarp.Log(e.Message);
                }
            }
        }
    }

    public class PathRoot : ScriptableObjectDictionary<string, List<Vector3>>
    {

    }
    //[System.Serializable]
    //public class PathRoot : ScriptableObject
    //{
    //    [SerializeField]
    //    public Dictionary<string, List<Vector3>> paths = new Dictionary<string, List<Vector3>>();
    //}

}
